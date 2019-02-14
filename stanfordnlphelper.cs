using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.ling;
using java.util;


using Newtonsoft.Json;
using edu.stanford.nlp.simple;


namespace Helpers
{
    public class NLPHelper
    {
        private const string defaultCulture = Culture.English;
        public static CRFClassifier Classifier = CRFClassifier.getClassifierNoExceptions(System.Web.HttpContext.Current.Server.MapPath(@"~/Assets/NLP/english.all.3class.distsim.crf.ser.gz"));

         public List<NLPTextResults> StanfordNLP(string words)
        {
            var response = "";

            var textresultsRecognizer = new List<NLPTextResults>();
            var textresultsStanfordNLP = new List<NLPTextResults>();

            var sent = new Sentence("Test");
            var nerTags = sent.nerTags().toArray();  
            String firstPOSTag = sent.posTag(0);   

            foreach (var item in nerTags)
            {
                var ItemTag = item;
            }
            
            try
            {
                var doc = new edu.stanford.nlp.simple.Document(words);
                var sentences = doc.sentences();
                foreach (CoreLabel item in sentences.toArray())
                {
                    var item2 = item.ner();
                    textresultsStanfordNLP.Add(new NLPTextResults { position = item.beginPosition(), text = item.word(), type = item.get(new CoreAnnotations.AnswerAnnotation().getClass()).ToString(), lemma = item.lemma(), processedBy = "StanFord NLP" });

                }
            }
            catch (Exception e)
            {                
                response += e.Message;
            }
            
            var resultsList = textresultsRecognizer.Concat(textresultsStanfordNLP);
            var SortedList = resultsList.OrderBy(o => o.position).ToList();           

            return (SortedList);
        }        

        
        public class NLPTextResults
        {
            public int position { get; set; }

            public string text { get; set; }

            public string type { get; set; }

            public string lemma { get; set; }

            public string processedBy { get; set; }
        }
                
    }
}
