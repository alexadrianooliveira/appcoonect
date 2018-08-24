using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaLoja.Models
{
    public class Token
    {
        private string _token = "";
        private string _token_type = "";
        private int _expiraEm = 0;
        private string _masterid = "0";
        private string _codpessoa = "0";
        private DateTime _dtExpira;

        public string access_token { get { return _token; } set { _token = value; } }
        public string token_type { get { return _token_type; } set { _token_type = value; } }
        public int expires_in { get { return _expiraEm; } set { _expiraEm = value; } }
        public string masterid { get { return _masterid; } set { _masterid = value; } }
        public string codpessoa { get { return _codpessoa; } set { _codpessoa = value; } }
        public DateTime DataExpira { get { return _dtExpira; } set { _dtExpira = value; } }
    }
}
