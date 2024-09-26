namespace MyBlog.WebUI.Util
{
    public static class StaticMethods
    {
        public static List<string> SplitSentences(string text)
        {
            string[] sentenceArray = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> sentences = new List<string>();
            foreach (var sentence in sentenceArray)
            {
                string trimmedSentence = sentence.Trim();
                sentences.Add(trimmedSentence);
            }

            return sentences;
        }
    }
}
