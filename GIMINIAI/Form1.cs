using System.Net.Http.Json;

namespace GIMINIAI
{
    public partial class Form1 : Form
    {
        private ChatHistory? _history;

        public Form1()
        {
            InitializeComponent();
        }

        private void sendBTN_Click(object sender, EventArgs e)
        {

        }
        private async Task SendMessageAsync()
        {
            var userMessage = questionTXT.Text.Trim();
            if (string.IsNullOrWhiteSpace(userMessage)) return;

            sendButton.Enabled = false;
            questionTXT.Enabled = false;

            chatDisplay.AppendText($"אתה: {userMessage}\n");

            _history!.AddUserMessage(userMessage);

            string response = await google_ai.Main(questionTXT.Text.ToString());
            //_history.AddAssistantMessage(response);
            //chatDisplay.AppendText($"Assistant: {response}\n\n");
            char[] responseArray = response.ToCharArray();
            for (int i = 0;i< responseArray.Length; i++)
            {
                if (responseArray[i] == '*')
                    responseArray[i] = ' ';
            }
            response = new string(responseArray);
            sendButton.Enabled = true;
            questionTXT.Enabled = true;
            questionTXT.Focus();
            chatDisplay.SelectionStart = chatDisplay.TextLength;
            chatDisplay.ScrollToCaret();
            chatDisplay.AppendText($"בוטי: {response}\n\n");
            questionTXT.Clear();
        }

        private void questionTXT_TextChanged(object sender, EventArgs e)
        {

        }

        private void chatDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void chatDisplay_TextChanged_1(object sender, EventArgs e)
        {
            //_history = new ChatHistory();

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessageAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                _history = new ChatHistory();
            _history.AddSystemMessage("You are a helpful assistant.");

        }
    }
}
