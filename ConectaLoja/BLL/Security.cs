using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConectaLoja.BLL
{
    public class Security
    {
        //private string URL = "http://localhost:1000/";
        private string URL = Utils.Constantes.URL;
        private string username = Utils.Constantes.USERAPI;
        private string pwdAPI = Utils.Constantes.PWDAPI;

        private string GetToken(string usuario, string senha)
        {
            string url = URL + "/token";

            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("username", usuario);
            parametros.Add("password", senha);
            parametros.Add("client_id", username);
            parametros.Add("client_secret", pwdAPI);
            parametros.Add("grant_type", "password");

            return Utils.Json.GetJSONString(url, parametros);
        }

        /// <summary>
        /// Valida para ver se o e-mail e a senha digitada pelo lojista é valido
        /// </summary>
        /// <param name="usuario">E-mail de acesso do usuário master</param>
        /// <param name="senha">Senha</param>
        /// <returns>ID do usuário Master, se devolver zero indica que os dados não conferem</returns>
        public int checkStore(string usuario, string senha)
        {
            string token = GetToken(usuario, senha);
            bool erro = token.IndexOf("error") >= 0;

            if (erro)
                return 0;
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Models.Token obj = serializer.Deserialize<Models.Token>(token);   
                int masterID = Utils.Valida.LimpaString(obj.masterid);
                obj.DataExpira = DateTime.Now.AddSeconds(obj.expires_in);
                obj.masterid = masterID.ToString();

                //Grava ou atualiza os dados no sqlite
                GravaLoja(obj, usuario, senha);


                return masterID;
            }
        }

        public Models.Loja CarregaDadosSalvos()
        {
            string strquery = "Select * FROM loja order by MasterID desc";
            DataTable dt = Dados.ExecutaSQLDataTable(strquery);
            Models.Loja loja = new Models.Loja();

            if (dt.Rows.Count > 0)
            {
                loja.Email = dt.Rows[0]["Nome"].ToString();
                loja.Senha = dt.Rows[0]["Senha"].ToString();
                loja.MasterID = Convert.ToInt32(dt.Rows[0]["MasterID"]);
            }
            return loja;
        }

        public void ApagaDadosLoja()
        {
            string strquery = "Delete FROM loja";
            Dados.ExecutaSQLNonQuery(strquery);
        }


        private void GravaLoja(Models.Token obj, string nome, string senha)
        {
            string strquery = "Select count(*) FROM loja where MasterID = " + obj.masterid;
            bool tem = Convert.ToInt32(Dados.ExecutaSQLScalar(strquery)) > 0;

            if (tem)
                strquery = "UPDATE loja SET Nome=@nome,Senha=@senha,Token=@token,Expira=@expira WHERE MasterID=@masterID";
            else
                strquery = "INSERT INTO loja (Nome,Senha,MasterID,Token,Expira)Values(@nome,@senha,@masterID,@token,@expira)";

            IDbDataParameter p1 = Dados.GetParameter("@nome", nome);
            IDbDataParameter p2 = Dados.GetParameter("@senha", senha);
            IDbDataParameter p3 = Dados.GetParameter("@masterID", obj.masterid);
            IDbDataParameter p4 = Dados.GetParameter("@token", obj.access_token);
            IDbDataParameter p5 = Dados.GetParameter("@expira", obj.DataExpira.ToString());

            Dados.ExecutaSQLNonQuery(strquery, p1, p2, p3, p4, p5);
        }
    }
}
