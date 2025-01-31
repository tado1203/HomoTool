using HomoTool.Extensions;
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
using VRC.Core;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
	public class ESP : ModuleBase
	{
		private List<GameObject> navMeshAgentGameObjects = new List<GameObject>();
		private List<GameObject> pickups = new List<GameObject>();
		private List<GameObject> fileFolders = new List<GameObject>();
		private List<GameObject> generators = new List<GameObject>();

		private bool renderNavMeshAgent = true;
		private bool renderPickup = false;
		private bool renderFileFolders = false;
		private bool renderGenerator = false;
		private float maxRenderDistance = 400f;

		public ESP() : base("ESP", false, true, KeyCode.F1) { }

		public override void OnGUI()
		{
			if (Networking.LocalPlayer == null || !Enabled)
				return;

			Vector3 localPlayerPosition = Networking.LocalPlayer.gameObject.transform.position;

			RenderObjects(pickups, renderPickup, Color.blue, localPlayerPosition);
			RenderPlayers(localPlayerPosition);
			RenderObjects(navMeshAgentGameObjects, renderNavMeshAgent, Color.red, localPlayerPosition);
			RenderObjects(fileFolders, renderFileFolders, new Color(0f, 163f / 255f, 175f / 255f), localPlayerPosition);
			RenderObjects(generators, renderGenerator, new Color(1f, 165f / 255f, 0f), localPlayerPosition);
		}

		private void RenderObjects(List<GameObject> gameObjects, bool renderFlag, Color color, Vector3 localPlayerPosition)
		{
			if (!renderFlag)
				return;

			foreach (var gameObject in gameObjects)
			{
				if (!gameObject.activeSelf)
					continue;

				float distance = Vector3.Distance(localPlayerPosition, gameObject.transform.position);
				if (distance > maxRenderDistance)
					continue;

				RenderGameObject(gameObject, color);
			}
		}

		private void RenderGameObject(GameObject gameObject, Color color)
		{
			Vector3 originPos = gameObject.transform.position;
			Vector3 upperPos = new Vector3(originPos.x, originPos.y + 1f, originPos.z);

			Vector3 screenFootPos = Camera.main.WorldToScreenPoint(originPos);
			Vector3 screenHeadPos = Camera.main.WorldToScreenPoint(upperPos);

			if (screenFootPos.z > 0f)
				RenderBoxESP(screenFootPos, screenHeadPos, color);
		}

		private void RenderPlayers(Vector3 localPlayerPosition)
		{
			foreach (var player in VRCPlayerApi.AllPlayers)
			{
				if (player.isLocal)
					continue;

				float distance = Vector3.Distance(localPlayerPosition, player.gameObject.transform.position);
				if (distance > maxRenderDistance)
					continue;

				RenderPlayer(player);
			}
		}

		private void RenderPlayer(VRCPlayerApi player)
		{
			Vector3 footPos = player.gameObject.transform.position;
			Vector3 headPos = new Vector3(footPos.x, footPos.y + player.GetAvatarEyeHeightAsMeters(), footPos.z);

			Vector3 screenFootPos = Camera.main.WorldToScreenPoint(footPos);
			Vector3 screenHeadPos = Camera.main.WorldToScreenPoint(headPos);

			if (screenFootPos.z > 0f)
			{
				RenderBoxESP(screenFootPos, screenHeadPos, player.GetPlayer().prop_APIUser_0.GetPlayerColor());

				string playerName = player.displayName;
				Vector2 textSize = RenderHelper.GetTextSize(playerName, 12);

				float textX = screenHeadPos.x - (textSize.x / 2f);
				float textY = (float)Screen.height - screenHeadPos.y - textSize.y;

				RenderHelper.RenderText(new Vector2(textX, textY), playerName, 12, Color.white, true);
			}
		}

		public override void OnMenu()
		{
			renderNavMeshAgent = GUILayout.Toggle(renderNavMeshAgent, "NavMeshAgent");
			renderPickup = GUILayout.Toggle(renderPickup, "Pickup");
			renderFileFolders = GUILayout.Toggle(renderFileFolders, "FileFolders");
			renderGenerator = GUILayout.Toggle(renderGenerator, "Generators");
			GUILayout.Label($"Max Render Distance: {maxRenderDistance:F1}m");
			maxRenderDistance = GUILayout.HorizontalSlider(maxRenderDistance, 10f, 500f);
		}

		public override void OnEnable()
		{
			if (Networking.LocalPlayer == null)
				return;

			ClearLists();

			foreach (var monster in Resources.FindObjectsOfTypeAll<NavMeshAgent>())
			{
				navMeshAgentGameObjects.Add(monster.gameObject);
			}
			foreach (var pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
			{
				pickups.Add(pickup.gameObject);
			}

			if (renderFileFolders)
			{
				AddFileFolderAndGenerators();
			}
		}

		public override void OnDisable()
		{
			if (Networking.LocalPlayer == null)
				return;

			ClearLists();
		}

		private void ClearLists()
		{
			navMeshAgentGameObjects.Clear();
			pickups.Clear();
			fileFolders.Clear();
			generators.Clear();
		}

		private void AddFileFolderAndGenerators()
		{
			foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
			{
				if (obj.name.Contains("SC_File"))
				{
					fileFolders.Add(obj);
				}
				if (obj.name.Contains("SC_generator"))
				{
					generators.Add(obj);
				}
			}
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