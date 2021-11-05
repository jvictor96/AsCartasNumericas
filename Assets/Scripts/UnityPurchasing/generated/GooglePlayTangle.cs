// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("vCYab/tR8gaB2VPYZWkANGfnVc/sE7Y4el9/LK73IW7S+HBEhP/ep7qHtOdSMY3ituvHRT4uU7/t9pcJfD4UtucjIYYj/FdmTlVECctCh1ppJbZeaCFfWypEhduvp0e7dHar3u5IAIXrGAheEuSRjetHGfzEAXI/EqsPBV5EMkhxJmdz9JU6vtRPxhneN82jjO7l2z3GMxCJ3H4j8qRtZS2XUzEqcgKvRE/ifYKdn9eFqj3Siz7/XNb2GZamwUj4/osbrzVsS7ofZ44v0cKdS/8s8eIJCS1p+ElcKem8LFXrCVDocnCNNXkG0eUtSIOgbe7g799t7uXtbe7u730eijKuSeffbe7N3+Lp5sVpp2kY4u7u7urv7HN4NJtNE6JvUO3s7u/u");
        private static int[] order = new int[] { 5,3,2,7,9,8,9,11,9,13,10,12,12,13,14 };
        private static int key = 239;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
