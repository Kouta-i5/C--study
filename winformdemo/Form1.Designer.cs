namespace WinFormsApp1
{
    partial class Form1
    //partialはクラスを分割して定義するためのキーワード
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true。そうでない場合は false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード
        //#regionはコードの範囲を指定するためのキーワード


        //InitializeComponentはForm1クラスのコンストラクター
        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //labelTimeは時間を表示するラベル
            this.labelTime = new System.Windows.Forms.Label();
            //buttonStartStopはスタートボタン
            this.buttonStartStop = new System.Windows.Forms.Button();
            //timer1はタイマー、componentsはコンポーネント、this.componentsはコンポーネントのインスタンス
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTime.Location = new System.Drawing.Point(35, 20);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(95, 45);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "25:00";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(46, 79);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(90, 28);
            this.buttonStartStop.TabIndex = 1;
            this.buttonStartStop.Text = "スタート";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            //buttonStartStop_Clickはスタートボタンをクリックしたときのイベントハンドラー
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            //timer1_Tickはタイマーが1秒ごとに実行されるイベントハンドラー
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 140);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.labelTime);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Timer timer1;
    }
}


