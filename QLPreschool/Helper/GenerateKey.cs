namespace QLPreschool.Helper
{
    public static class GenerateKey
    {
        public static string GenerateKeyPrimary(string key,int count)
        {
            
            string oldStr = key.Substring(0, count);
            int numFinal = int.Parse(key.Substring(count,1));
            string newKey = $"{oldStr}{++numFinal}";
            return newKey;
        }
    }
}
