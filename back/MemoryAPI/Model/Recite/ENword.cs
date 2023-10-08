using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Recite
{
    public class ENword
    {
        public int word_id { get; set; }
        public string? englishWord { get; set; }
        public string? chineseWord { get; set; }
        public string? englishInstance1 { get; set; }
        public string? chineseInstance1 { get; set; }
        public string? englishInstance2 { get; set; }
        public string? chineseInstance2 { get; set; }
        public string? pron { get; set; }
        public string? lexicon { get; set; }

    }
    public class ENwordplus: ENword
    {
        public int count { get; set; }
        public int isknow { get; set; }


    }
}
