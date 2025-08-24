using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    //Form1はFormクラスを継承している,FormクラスはWindows Formsの基本クラス、Form1はFormクラスのインスタンスを作成している
    //publicはクラスの外部からアクセスできることを表す
    //partialはクラスを分割して定義するためのキーワード
    //public partialでクラスを分割して定義することができる、Form1.Designer.csとForm1.csの2つのファイルに分かれている
    {
        //privateはクラスの内部からアクセスできることを表す
        //constは定数を定義するためのキーワード
        //WorkMinutesは25分を表す定数
        //SecondsPerMinuteは1分を60秒に変換するための定数
        //_isRunningはプログラムが実行中かどうかを表す変数
        //_remainingSecondsは残り時間を表す変数
        private const int WorkMinutes = 25;
        private const int SecondsPerMinute = 60;
        private bool _isRunning = false;
        private int _remainingSeconds = WorkMinutes * SecondsPerMinute;  // 25分


        //InitializeComponentはForm1クラスのコンストラクター
        public Form1()
        {
            InitializeComponent();
        }
        //

        private void buttonStartStop_Click(object sender, EventArgs e)
        //buttonStartStop_Clickはスタートボタンをクリックしたときのイベントハンドラー
        {
            
            if (!_isRunning)
            {
                timer1.Start();
                buttonStartStop.Text = "ストップ";
            }
            else
            {
                timer1.Stop();
                buttonStartStop.Text = "スタート";
            }
            _isRunning = !_isRunning;
        }

        private void timer1_Tick(object sender, EventArgs e)
        //timer1_Tickはタイマーが1秒ごとに実行されるイベントハンドラー
        {
            //_remainingSecondsは残り時間を表す変数、--は1減らすということ
            //_remainingSeconds--;
            //_remainingSeconds -= 1;
            _remainingSeconds = _remainingSeconds - 1;
            if (_remainingSeconds < 0)
            {
                timer1.Stop();
                MessageBox.Show("時間になりました！");
                _remainingSeconds = WorkMinutes * SecondsPerMinute;  // リセット
            }

            int minutes = _remainingSeconds / SecondsPerMinute;
            int seconds = _remainingSeconds % SecondsPerMinute;
            labelTime.Text = $"{minutes:00}:{seconds:00}";
        }
    }
}


