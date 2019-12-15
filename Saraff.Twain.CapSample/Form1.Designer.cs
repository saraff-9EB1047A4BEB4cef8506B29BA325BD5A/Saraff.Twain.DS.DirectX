/*  This file is part of the Saraff DirectX DS.
 *
 *  The Saraff DirectX DS is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  The Saraff DirectX DS is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with the Saraff DirectX DS.  If not, see <https://www.gnu.org/licenses/>.
 *
 * Этот файл — часть Saraff DirectX DS.
 *
 * Saraff DirectX DS - свободная программа: вы можете перераспространять ее и/или
 * изменять ее на условиях Стандартной общественной лицензии GNU в том виде,
 * в каком она была опубликована Фондом свободного программного обеспечения;
 * либо версии 3 лицензии, либо (по вашему выбору) любой более поздней
 * версии.
 *
 * Saraff DirectX DS распространяется в надежде, что она будет полезной,
 * но БЕЗО ВСЯКИХ ГАРАНТИЙ; даже без неявной гарантии ТОВАРНОГО ВИДА
 * или ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. Подробнее см. в Стандартной
 * общественной лицензии GNU.
 *
 * Вы должны были получить копию Стандартной общественной лицензии GNU
 * вместе с этой программой. Если это не так, см. <https://www.gnu.org/licenses/>.
 */
namespace Saraff.Twain.CapSample {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.acquireToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dsToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deviceToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.sizeToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.aToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.twain32 = new Saraff.Twain.Twain32(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStripPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripPanel1
            // 
            this.toolStripPanel1.Controls.Add(this.toolStrip1);
            this.toolStripPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.toolStripPanel1.Name = "toolStripPanel1";
            this.toolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripPanel1.Size = new System.Drawing.Size(800, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acquireToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.dsToolStripComboBox,
            this.toolStripSeparator2,
            this.deviceToolStripComboBox,
            this.sizeToolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(5, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(582, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // acquireToolStripButton
            // 
            this.acquireToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.acquireToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("acquireToolStripButton.Image")));
            this.acquireToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.acquireToolStripButton.Name = "acquireToolStripButton";
            this.acquireToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.acquireToolStripButton.Text = "Acquire";
            this.acquireToolStripButton.Click += new System.EventHandler(this._AcquireClick);
            // 
            // savetoolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("savetoolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "savetoolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this._SaveClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // dsToolStripComboBox
            // 
            this.dsToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dsToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dsToolStripComboBox.Name = "dsToolStripComboBox";
            this.dsToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // deviceToolStripComboBox
            // 
            this.deviceToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceToolStripComboBox.Name = "deviceToolStripComboBox";
            this.deviceToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            this.deviceToolStripComboBox.Visible = false;
            // 
            // sizeToolStripDropDownButton
            // 
            this.sizeToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sizeToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem1,
            this.bToolStripMenuItem1});
            this.sizeToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("sizeToolStripDropDownButton.Image")));
            this.sizeToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sizeToolStripDropDownButton.Name = "sizeToolStripDropDownButton";
            this.sizeToolStripDropDownButton.Size = new System.Drawing.Size(77, 22);
            this.sizeToolStripDropDownButton.Text = "WxH@BPP";
            this.sizeToolStripDropDownButton.Visible = false;
            // 
            // aToolStripMenuItem1
            // 
            this.aToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aAToolStripMenuItem1});
            this.aToolStripMenuItem1.Name = "aToolStripMenuItem1";
            this.aToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.aToolStripMenuItem1.Text = "A";
            // 
            // aAToolStripMenuItem1
            // 
            this.aAToolStripMenuItem1.Name = "aAToolStripMenuItem1";
            this.aAToolStripMenuItem1.Size = new System.Drawing.Size(90, 22);
            this.aAToolStripMenuItem1.Text = "AA";
            // 
            // bToolStripMenuItem1
            // 
            this.bToolStripMenuItem1.Name = "bToolStripMenuItem1";
            this.bToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.bToolStripMenuItem1.Text = "B";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // twain32
            // 
            this.twain32.AppProductName = "Saraff.Twain.NET";
            this.twain32.IsTwain2Enable = true;
            this.twain32.Parent = this;
            this.twain32.ShowUI = false;
            this.twain32.AcquireError += new System.EventHandler<Saraff.Twain.Twain32.AcquireErrorEventArgs>(this._AcquireError);
            this.twain32.EndXfer += new System.EventHandler<Saraff.Twain.Twain32.EndXferEventArgs>(this._EndXfer);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 425);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "jpg";
            this.saveFileDialog1.Title = "Save image...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStripPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStripPanel1.ResumeLayout(false);
            this.toolStripPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel toolStripPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton acquireToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox dsToolStripComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripDropDownButton sizeToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aAToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox deviceToolStripComboBox;
        private Saraff.Twain.Twain32 twain32;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

