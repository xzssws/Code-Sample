using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TrainingProject.JSON
{
    public partial class JSON序列化 : BaseForm
    {
        public JSON序列化()
        {
            InitializeComponent();
            AddCommand("序列化", Serviser);
            AddCommand("反序列化", DeServiser);
        }
        private void Serviser()
        {
            Studnet stu = new Studnet() { ID = txtID.Text, Name = txtName.Text, Classs = txtClasses.Text };
            string JsonStr = JSON.JsonHelp.ToJson<Studnet>(stu);
            txtJSON.Text = JsonStr;
        }
        private void DeServiser()
        {
            Studnet stu = (Studnet)JSON.JsonHelp.FromJsonX<Studnet>(txtJSON.Text);
            txtID.Text = stu.ID;
            txtName.Text = stu.Name;
            txtClasses.Text = stu.Classs;
        }
    

    }
    //测试通过
    public static class JsonHelp
    {
        /// <summary>
        /// 对象序列化到JSON串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">对应类型的对象</param>
        /// <returns>Json串</returns>
        public static string ToJsonX<T>(T value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None);
        }

        /// <summary>
        /// JSON串反序列化到对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonText">JSON串</param>
        /// <returns>对应类型的对象</returns>
        public static T FromJsonX<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText); ;
        }

        /// <summary>
        /// 对象序列化到JSON串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">对应类型的对象</param>
        /// <returns>Json串</returns>
        public static string ToJson<T>(T value)
        {
            //新建一个JSON序列化对象
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            StringWriter sw = new StringWriter();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Formatting.None;
            writer.QuoteChar = '"';
            json.Serialize(writer, value);
            string output = sw.ToString();
            writer.Close();
            sw.Close();
            return output;
        }

        /// <summary>
        /// JSON串反序列化到对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="JSONText">JSON串</param>
        /// <returns>对应类型的对象</returns>
        public static T FromJson<T>(string JSONText)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            StringReader sr = new StringReader(JSONText);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            T result = (T)json.Deserialize(reader, typeof(T));
            reader.Close();
            return result;
        }
    }

    public class Studnet
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Classs { get; set; }
    }
}