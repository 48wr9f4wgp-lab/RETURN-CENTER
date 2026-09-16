#if UNITY_EDITOR
using System;
using ReturnCenter.Application;
using ReturnCenter.Core.Domain;
using UnityEditor;
using UnityEngine;

namespace ReturnCenter.Editor
{
    public static class ReturnCenterSmokeTests
    {
        [MenuItem("RETURN CENTER/Run Core Smoke Tests")]
        public static void RunFromMenu() => Run();

        public static void RunFromCommandLine()
        {
            Run();
            EditorApplication.Exit(0);
        }

        private static void Run()
        {
            var product = new Product("test-1", "Coffee Maker", 120, ProductCondition.B, FaultType.PowerFailure, 22);
            var session = new ManualProcessingSession(product, 100);

            Assert(product.State == ProductLifecycleState.Received, "product begins Received");
            session.Inspect();
            Assert(product.State == ProductLifecycleState.Inspected, "inspection transitions state");
            Assert(session.GetRecommendation() == DispositionRoute.Repair, "power failure sample recommends Repair");

            var outcome = session.Process(DispositionRoute.Repair);
            Assert(product.State == ProductLifecycleState.Processed, "processing transitions state");
            Assert(outcome.NetProfit > 0, "sample repair has positive net profit");
            Assert(session.Economy.ProcessedCount == 1, "economy processed count increments");
            Assert(session.Economy.Cash > 100, "economy cash reflects profitable processing");

            Debug.Log("RETURN CENTER core smoke tests: PASS");
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition) throw new Exception("RETURN CENTER smoke test failed: " + message);
        }
    }
}
#endif
