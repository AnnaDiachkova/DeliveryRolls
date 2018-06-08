using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary3
{
    public class Xml
    {
        public List<Treatment> DeSerializer(string FileOpen, List<Treatment> HumanTreat)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(List<Treatment>));
            using (FileStream fs = new FileStream(FileOpen, FileMode.OpenOrCreate))
            {
                HumanTreat = (List<Treatment>)formatter.Deserialize(fs);
            }
            return HumanTreat;
        }
        public void Serializer(string FileSave, List<Treatment> HumanTreat)
        {
            XmlSerializer formatter2 = new XmlSerializer(typeof(List<Treatment>));
            File.Delete(FileSave);
            using (FileStream fs = new FileStream(FileSave, FileMode.OpenOrCreate))
            {
                formatter2.Serialize(fs, HumanTreat);
            }
        }
    }
}
