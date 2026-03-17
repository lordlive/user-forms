using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace UsersForms
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //объявляем список объектов типa UserClass
            List<UserClass> Users = new List<UserClass>();
            //проверяем наличие дайла с данными
            if (File.Exists("user.xml"))
            {
                Users = DeSerialize(); // если файл есть читаем данные
            }
            else // если нет создаем по умолчанию
            {
                Users.Add(new UserClass("admin", "admin1234", true, true));
                Users.Add(new UserClass("user1", "qwerty123", true, false));
                Serialize(Users); //и сохраняем
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(Users));
        }
        public static void Serialize(List<UserClass> temp)
        {
            // передаем в конструктор тип класса
            // получаем поток, куда будем записывать сериализованный объект            
            FileStream fs = new FileStream("user.xml", FileMode.Create);
            // создаем объект XmlSerializer            
            XmlSerializer formatter = new XmlSerializer(typeof(List<UserClass>));
            try
            {
                formatter.Serialize(fs, temp);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Объект не сериализован основание: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        static List<UserClass> DeSerialize()
        {
            // создаем временный объект для десериализации
            List<UserClass> lists = new List<UserClass>();
            //открываем поток из файла        
            FileStream fs = new FileStream("user.xml", FileMode.OpenOrCreate);
            try
            {
                // создаем объект XmlSerializer
                XmlSerializer formatter = new XmlSerializer(typeof(List<UserClass>));
                lists = (List<UserClass>)formatter.Deserialize(fs);
                return lists;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Объект не десериализован основание: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();// закрываем поток
            }
        }
    }    
}
