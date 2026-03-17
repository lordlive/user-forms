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
    public partial class LoginForms : Form
    {
        //Событие для передачи данных
        public event EventHandler<UserEventArgs> sendDataFormEvent;
        //Конструктор формы
        public LoginForms()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //создаем объект типа UserClass
            UserClass temp = new UserClass(textBoxLogin.Text, textBoxPassword.Text);
            //Генерируем событие с именованным аргументом
            //в класс аргумента передаем созданный объект
            if (sendDataFormEvent != null)
                sendDataFormEvent(this, new UserEventArgs(temp));

            //Закрываем форму
            this.Close();
        }
    }
}
