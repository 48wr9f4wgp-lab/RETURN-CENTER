using ReturnCenter.Application;
using ReturnCenter.Core.Domain;
using UnityEngine;

namespace ReturnCenter.Presentation
{
    public sealed class ReturnCenterBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            BuildGreyboxWarehouse();
            var player = BuildPlayer();
            BuildManualReturnPackage();
            var hud = player.gameObject.AddComponent<BootstrapHud>();
            hud.Initialize(player.GetComponent<InteractionController>());
        }

        private static FirstPersonController BuildPlayer()
        {
            var go = new GameObject("Player");
            go.transform.position = new Vector3(0f, 1f, -5f);
            var controller = go.AddComponent<CharacterController>();
            controller.height = 1.8f;
            controller.radius = 0.35f;
            controller.center = new Vector3(0f, 0.9f, 0f);

            var cameraGo = new GameObject("ViewCamera");
            cameraGo.transform.SetParent(go.transform, false);
            cameraGo.transform.localPosition = new Vector3(0f, 1.65f, 0f);
            cameraGo.AddComponent<Camera>();

            var fps = go.AddComponent<FirstPersonController>();
            go.AddComponent<InteractionController>();
            return fps;
        }

        private static void BuildManualReturnPackage()
        {
            CreateCube("InspectionDesk", new Vector3(0f, 0.55f, 1.8f), new Vector3(2.5f, 1.1f, 1.2f), new Color(0.20f, 0.28f, 0.35f));
            var package = CreateCube("ReturnPackage", new Vector3(0f, 1.45f, 1.8f), new Vector3(0.9f, 0.55f, 0.7f), new Color(0.72f, 0.46f, 0.20f));

            var product = new Product(
                "RC-VS-0001",
                "Coffee Maker",
                120.0,
                ProductCondition.B,
                FaultType.PowerFailure,
                22.0);
            var session = new ManualProcessingSession(product, 100.0);
            package.AddComponent<ReturnPackageInteractable>().Initialize(session);

            CreateRouteMarker("RESELL", new Vector3(-4.5f, 0.25f, 3.5f), new Color(0.25f, 0.65f, 0.35f));
            CreateRouteMarker("REPAIR", new Vector3(0f, 0.25f, 3.5f), new Color(0.25f, 0.45f, 0.80f));
            CreateRouteMarker("RECYCLE", new Vector3(4.5f, 0.25f, 3.5f), new Color(0.70f, 0.35f, 0.25f));
        }

        private static void BuildGreyboxWarehouse()
        {
            Camera.main?.gameObject.SetActive(false);
            CreateCube("Floor", new Vector3(0f, -0.15f, 2f), new Vector3(16f, 0.3f, 16f), new Color(0.38f, 0.40f, 0.42f));
            CreateCube("BackWall", new Vector3(0f, 2f, 9.8f), new Vector3(16f, 4f, 0.25f), new Color(0.52f, 0.54f, 0.56f));
            CreateCube("LeftWall", new Vector3(-7.9f, 2f, 2f), new Vector3(0.25f, 4f, 16f), new Color(0.52f, 0.54f, 0.56f));
            CreateCube("RightWall", new Vector3(7.9f, 2f, 2f), new Vector3(0.25f, 4f, 16f), new Color(0.52f, 0.54f, 0.56f));

            var lightGo = new GameObject("WarehouseLight");
            var light = lightGo.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.4f;
            lightGo.transform.rotation = Quaternion.Euler(48f, -30f, 0f);
        }

        private static GameObject CreateCube(string name, Vector3 position, Vector3 scale, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = scale;
            var renderer = go.GetComponent<Renderer>();
            renderer.material.color = color;
            return go;
        }

        private static void CreateRouteMarker(string label, Vector3 position, Color color)
        {
            var marker = CreateCube(label, position, new Vector3(2.4f, 0.5f, 2.4f), color);
            marker.GetComponent<Collider>().enabled = false;
        }
    }
}
