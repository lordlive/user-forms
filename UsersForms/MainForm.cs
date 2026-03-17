using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersForms
{
    public partial class MainForm : Form
    {
        //статическая переменная класа в которую записываются данные с формы для ввода логина и пароля
        public static UserClass login = new UserClass();
        //статическая переменная определяющая текущее положение в списке пользователей
        public static int CountList = 0;
        public static List<UserClass> Work; //список пользоветелей 
        public MainForm(List<UserClass> Users)

        {
            // первой формой должна быть форма проверки логина и пароля и только после успешной аутендификациии
            //запускается основная форма!!!!
            //List<UserClass> Base = Users;
            Work = Users;  // присваеваем списку пользователей полученный список из стартовой программы
            bool accsesLog = false; // переменная о правильности логина
            bool accsesPas = false; // переменная о првильности пароля 

            InitializeComponent();
            //создаем форму ввода логина и пароля
            LoginForms pForm = new LoginForms();
            //Подключение обработчика события в дочерней форме
            pForm.sendDataFormEvent += new EventHandler<UserEventArgs>(pForm_sendDataFormEvent);            
            //Выводим ее для заполнения текстовых полей
            pForm.ShowDialog();
            //проверяем коректность ввода пользователя и пароля
            foreach (UserClass temp in Work)
                if (login.Name == temp.Name) // проверяем есть ли такой логин
                {
                    accsesLog = true;
                    if (login.Password == temp.Password) // проверяем соответствует ли ему пароль
                    {
                        accsesPas = true;
                        login.aUser = temp.aUser;
                    }
                }
                if (accsesLog != accsesPas || !accsesLog)
                {
                    // выводим сообщение о неверноми логине и пароле
                    MessageBox.Show("Логин или пароль не верны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //после нажатия на кнопку (ок) закрываем приложение
                    Environment.Exit(0); // закрываем приложение
                }
            UserClass workUser = login; //текущий пользователь
            if (!workUser.aUser)
            {
                //если пользователь не администратор то запрет доступа к кнопкам
                buttonAdd.Enabled = false;
                buttonSave.Enabled = false;
                buttonDel.Enabled = false;
            }
            login = null; //обнуляем ссылку 

            //Выводим информацию о текущем пользователе 
            //Выводим имя пользователя
            textBoxName.Text = Work[CountList].Name.ToString();
            //выводим тип пользователя
            if (Work[CountList].aUser)
                textBoxType.Text = "Administrator";
            else
                textBoxType.Text = "User";
        }
        //Обработчик события получения данных из дочерней формы
        void pForm_sendDataFormEvent(object sender, UserEventArgs e)
        {
            //получаем объект из класса
            login = e.SendingUser;
            //MessageBox.Show(login.Name);
            //textBoxLog.Text = login.Name;
            //textBoxPass.Text = login.Password;
        }        
        // Нажата кнопка просмотра следующего елемента списка пользователей
        private void buttonNext_Click(object sender, EventArgs e)
        {
            //провенряем что не конец списка и увеличиваем счетчик на 1 
            if (CountList < Work.Count-1)
                CountList++;
            //Выводим имя пользователя
            textBoxName.Text = Work[CountList].Name.ToString();
            //выводим тип пользователя
            if (Work[CountList].aUser)
                textBoxType.Text = "Administrator";
            else
                textBoxType.Text = "User";
        }
        // Нажата кнопка просмотра предыдущего елемента списка пользователей
        private void buttonPrevius_Click(object sender, EventArgs e)
        {
            //провенряем что не начало списка и уменьшаем счетчик на 1 
            if (CountList > 0)
                CountList--;
            //Выводим имя пользователя
            textBoxName.Text = Work[CountList].Name.ToString();
            //выводим тип пользователя
            if (Work[CountList].aUser)
                textBoxType.Text = "Administrator";
            else
                textBoxType.Text = "User";
        }
        // нажата кнопка добавления нового пользователя в список
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddForms pForm = new AddForms();
            //Подключение обработчика события в дочерней форме
            pForm.sendDataFormEvent += new EventHandler<UserEventArgs>(pForm_sendDataFormEvent);
            //Выводим ее для заполнения текстовых полей
            pForm.ShowDialog();
            Work.Add(login);
        }
        //нажата кнопка сохранить(сохраняем текущую базу пользователей)
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Program.Serialize(Work);
        }
        //нажата кнопка удаления выбранного пользователя
        private void buttonDel_Click(object sender, EventArgs e)
        {
            //проверяем что счетчик находится внутри списка и удаляем текущую запись
            if (CountList >= 0 & CountList < Work.Count - 1)
                if (Work[CountList].dUser != true) // проверяем что выбранный для удаления пользователь не является пользователем по умолчанию
                    Work.RemoveAt(CountList);

        }
    }
}
