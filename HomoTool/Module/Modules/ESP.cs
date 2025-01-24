using HomoTool.Helpers;
using Il2CppInterop.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
	public class ESP : ModuleBase
	{
		private bool renderNavMeshAgent = true;
		private List<GameObject> navMeshAgentGameObjects = new List<GameObject>();
		private float maxRenderDistance = 200f;

		public ESP() : base("ESP", false, true, KeyCode.F1) { }

		public override void OnGUI()
		{
			if (Networking.LocalPlayer == null || !Enabled)
				return;

			Vector3 localPlayerPosition = Networking.LocalPlayer.gameObject.transform.position;

			foreach (var player in VRCPlayerApi.AllPlayers)
			{
				if (player.isLocal)
					continue;

				float distance = Vector3.Distance(localPlayerPosition, player.gameObject.transform.position);
				if (distance > maxRenderDistance)
					continue;

				Vector3 footPos = player.gameObject.transform.position;
				Vector3 headPos = new Vector3(footPos.x, footPos.y + player.GetAvatarEyeHeightAsMeters(), footPos.z);

				Vector3 screenFootPos = Camera.main.WorldToScreenPoint(footPos);
				Vector3 screenHeadPos = Camera.main.WorldToScreenPoint(headPos);

				if (screenFootPos.z > 0f)
				{
					RenderBoxESP(screenFootPos, screenHeadPos, Color.white);

					string playerName = player.displayName;
					Vector2 textSize = RenderHelper.GetTextSize(playerName, 12);

					float textX = screenHeadPos.x - (textSize.x / 2f);
					float textY = (float)Screen.height - screenHeadPos.y - textSize.y;

					RenderHelper.RenderText(new Vector2(textX, textY), playerName, 12, Color.white, true);
				}
				else
				{
					RenderOffscreenIndicator(player.gameObject.transform.position, Color.white);
				}
			}

			if (renderNavMeshAgent)
			{
				foreach (var gameObject in navMeshAgentGameObjects)
				{
					if (!gameObject.activeSelf)
						continue;

					float distance = Vector3.Distance(localPlayerPosition, gameObject.transform.position);
					if (distance > maxRenderDistance)
						continue;

					Vector3 footPos = gameObject.transform.position;
					Vector3 headPos = new Vector3(footPos.x, footPos.y + 2f, footPos.z);

					Vector3 screenFootPos = Camera.main.WorldToScreenPoint(footPos);
					Vector3 screenHeadPos = Camera.main.WorldToScreenPoint(headPos);

					if (screenFootPos.z > 0f)
						RenderBoxESP(screenFootPos, screenHeadPos, Color.red);
					else
						RenderOffscreenIndicator(gameObject.transform.position, Color.red);
				}
			}
		}

		public override void OnMenu()
		{
			renderNavMeshAgent = GUILayout.Toggle(renderNavMeshAgent, "NavMeshAgent");
			GUILayout.Label($"Max Render Distance: {maxRenderDistance:F1}m");
			maxRenderDistance = GUILayout.HorizontalSlider(maxRenderDistance, 10f, 500f);
		}

		public override void OnEnable()
		{
			if (Networking.LocalPlayer == null || !renderNavMeshAgent)
				return;

			navMeshAgentGameObjects.Clear();
			foreach (var monster in Resources.FindObjectsOfTypeAll<NavMeshAgent>())
			{
				navMeshAgentGameObjects.Add(monster.gameObject);
			}
		}

		public override void OnDisable()
		{
			if (Networking.LocalPlayer == null || !renderNavMeshAgent)
				return;

			navMeshAgentGameObjects.Clear();
		}

		private void RenderBoxESP(Vector3 footPos, Vector3 headPos, Color color)
		{
			float height = headPos.y - footPos.y;
			float width = height / 2f;

			float x = footPos.x - (width / 2f);
			float y = (float)Screen.height - headPos.y;

			Color oldColor = GUI.color;

			// Outer black outline
			GUI.color = Color.black;
			GUI.DrawTexture(new Rect(x - 1f, y - 1f, width + 2f, 1f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x - 1f, y + height, width + 2f, 1f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x - 1f, y - 1f, 1f, height + 2f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x + width, y - 1f, 1f, height + 2f), Texture2D.whiteTexture);

			// Inner black outline
			GUI.DrawTexture(new Rect(x + 1f, y + 1f, width - 2f, 1f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x + 1f, y + height - 2f, width - 2f, 1f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x + 1f, y + 1f, 1f, height - 2f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x + width - 2f, y + 1f, 1f, height - 2f), Texture2D.whiteTexture);

			// Main colored rectangle
			GUI.color = color;
			GUI.DrawTexture(new Rect(x, y, width, 1f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x, y + height - 1f, width, 1f), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x, y, 1f, height), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(x + width - 1f, y, 1f, height), Texture2D.whiteTexture);

			GUI.color = oldColor;
		}

		private void RenderOffscreenIndicator(Vector3 worldPosition, Color color)
		{
			Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

			if (screenPosition.z < 0)
			{
				screenPosition.x = Screen.width - screenPosition.x;
				screenPosition.y = Screen.height - screenPosition.y;
			}

			screenPosition.z = Mathf.Abs(screenPosition.z);

			Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Vector2 direction = new Vector2(screenPosition.x - screenCenter.x, screenPosition.y - screenCenter.y).normalized;

			float arrowSize = 20f;
			Vector2 arrowPosition = screenCenter + direction * (Mathf.Min(screenCenter.x, screenCenter.y) - arrowSize);

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			GUIUtility.RotateAroundPivot(angle, arrowPosition);
			GUI.color = color;
			GUI.DrawTexture(new Rect(arrowPosition.x - arrowSize / 2f, arrowPosition.y - arrowSize / 2f, arrowSize, arrowSize), Texture2D.whiteTexture);
			GUI.color = Color.white;
			GUIUtility.RotateAroundPivot(-angle, arrowPosition);
		}
	}
}
