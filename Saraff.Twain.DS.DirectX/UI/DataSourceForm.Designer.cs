﻿/*  This file is part of the Saraff DirectX DS.
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
namespace Saraff.Twain.DS.DirectX.UI {
    partial class DataSourceForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceForm));
            this.player = new AForge.Controls.VideoSourcePlayer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.doneButton = new System.Windows.Forms.Button();
            this.acquireButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.filterInfoViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.capabilityViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.transferImmediatelyCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterInfoViewBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capabilityViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player.Location = new System.Drawing.Point(0, 0);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(493, 386);
            this.player.TabIndex = 0;
            this.player.VideoSource = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(12, 64);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.player);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(658, 386);
            this.splitContainer1.SplitterDistance = 493;
            this.splitContainer1.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(161, 386);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // doneButton
            // 
            this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.doneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doneButton.Location = new System.Drawing.Point(595, 456);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 1;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this._DoneClick);
            // 
            // acquireButton
            // 
            this.acquireButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acquireButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acquireButton.Location = new System.Drawing.Point(514, 456);
            this.acquireButton.Name = "acquireButton";
            this.acquireButton.Size = new System.Drawing.Size(75, 23);
            this.acquireButton.TabIndex = 0;
            this.acquireButton.Text = "Acquire";
            this.acquireButton.UseVisualStyleBackColor = true;
            this.acquireButton.Click += new System.EventHandler(this._AcquireClick);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DataSource = this.filterInfoViewBindingSource;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(444, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // filterInfoViewBindingSource
            // 
            this.filterInfoViewBindingSource.DataSource = typeof(Saraff.Twain.DS.DirectX.UI.DataSourceForm.FilterInfoView);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 46);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resolution";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.DataSource = this.capabilityViewBindingSource;
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(531, 19);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 0;
            // 
            // capabilityViewBindingSource
            // 
            this.capabilityViewBindingSource.DataSource = typeof(Saraff.Twain.DS.DirectX.UI.DataSourceForm.CapabilityView);
            // 
            // transferImmediatelyCheckBox
            // 
            this.transferImmediatelyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transferImmediatelyCheckBox.AutoSize = true;
            this.transferImmediatelyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transferImmediatelyCheckBox.Location = new System.Drawing.Point(12, 460);
            this.transferImmediatelyCheckBox.Name = "transferImmediatelyCheckBox";
            this.transferImmediatelyCheckBox.Size = new System.Drawing.Size(181, 17);
            this.transferImmediatelyCheckBox.TabIndex = 2;
            this.transferImmediatelyCheckBox.Text = "Transfer immediately after acquire";
            this.transferImmediatelyCheckBox.UseVisualStyleBackColor = true;
            // 
            // DataSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 491);
            this.Controls.Add(this.transferImmediatelyCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.acquireButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "DataSourceForm";
            this.Text = "DataSourceForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filterInfoViewBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capabilityViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer player;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button acquireButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox transferImmediatelyCheckBox;
        private System.Windows.Forms.BindingSource filterInfoViewBindingSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.BindingSource capabilityViewBindingSource;
    }
}