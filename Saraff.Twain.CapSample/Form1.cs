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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saraff.Twain.CapSample {

    internal sealed partial class Form1 : Form {

        public Form1() {
            this.InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            try {
                this.twain32.OpenDSM();
                this.dsToolStripComboBox.Items.Clear();
                for(var i = 0; i < twain32.SourcesCount; i++) {
                    this.dsToolStripComboBox.Items.Add(this.twain32.GetSourceProductName(i));
                }
                if(this.dsToolStripComboBox.Items.Count > 0) {
                    this.dsToolStripComboBox.SelectedIndex = this.twain32.GetDefaultSource();
                }
                this.dsToolStripComboBox.SelectedIndexChanged += this._DsSelectedIndexChanged;
                this._LoadCaps();
            } catch(Exception ex) {
                this._ToLog(ex);
            }
        }

        private void _LoadCaps() {
            this.twain32.OpenDataSource();
            this.deviceToolStripComboBox.Items.Clear();
            this.sizeToolStripDropDownButton.DropDownItems.Clear();

            if(this.deviceToolStripComboBox.Visible = this.sizeToolStripDropDownButton.Visible =
                this.twain32.Capabilities.CustomInterfaceGuid.IsSupported(TwQC.GetCurrent) &&
                this.twain32.Capabilities.CustomInterfaceGuid.GetCurrent().ToLower().Trim('{', '}') == "e44c2dc8-363d-4dcb-8ef1-0ffd72b54673") {

                var _devices = this.twain32.GetCap(CustomCap.VideoDevices) as Twain32.Enumeration;
                for(var i = 0; i < _devices.Count; i++) {
                    this.deviceToolStripComboBox.Items.Add(_devices[i]);
                }
                if(_devices.Count > 0) {
                    this.deviceToolStripComboBox.SelectedIndex = 0;
                }
                this.deviceToolStripComboBox.SelectedIndexChanged += this._DeviceSelectedIndexChanged;
                this._LoadFrameSizes();
            }
        }

        private void _LoadFrameSizes() {
            this.sizeToolStripDropDownButton.DropDownItems.Clear();
            var _width = this.twain32.GetCap(CustomCap.FrameWidth) as Twain32.Enumeration;
            for(var i = 0; i < _width.Count; i++) {
                var _item = new ToolStripMenuItem(_width[i].ToString()) {
                    Tag = _width[i]
                };

                this.twain32.SetCap(CustomCap.FrameWidth, _width[i]);
                var _height = this.twain32.GetCap(CustomCap.FrameHeight) as Twain32.Enumeration;
                for(var ii = 0; ii < _height.Count; ii++) {
                    var _subitem = new ToolStripMenuItem(_height[ii].ToString()) {
                        Tag = _height[ii]
                    };

                    this.twain32.SetCap(CustomCap.FrameHeight, _height[ii]);
                    var _bpp = this.twain32.GetCap(CustomCap.FrameBpp) as Twain32.Enumeration;
                    for(var iii = 0; iii < _bpp.Count; iii++) {
                        var _bppitem = new ToolStripMenuItem(_bpp[iii].ToString(), null, (sender, e) => {
                            this.twain32.SetCap(CustomCap.FrameWidth, (sender as ToolStripItem).OwnerItem.OwnerItem.Tag);
                            this.twain32.SetCap(CustomCap.FrameHeight, (sender as ToolStripItem).OwnerItem.Tag);
                            this.twain32.SetCap(CustomCap.FrameBpp, (sender as ToolStripItem).Tag);
                            this._ShowFrameSize();
                        }) {
                            Tag = _bpp[iii]
                        };
                        _subitem.DropDownItems.Add(_bppitem);
                    }

                    _item.DropDownItems.Add(_subitem);
                }

                this.sizeToolStripDropDownButton.DropDownItems.Add(_item);
            }
            this.twain32.SetCap(CustomCap.FrameWidth, _width[0]);
            this._ShowFrameSize();
        }

        private void _ShowFrameSize() => this.sizeToolStripDropDownButton.Text = $"{this.twain32.GetCurrentCap(CustomCap.FrameWidth)} x {this.twain32.GetCurrentCap(CustomCap.FrameHeight)} @ {this.twain32.GetCurrentCap(CustomCap.FrameBpp)}";

        private void _DsSelectedIndexChanged(object sender, EventArgs e) {
            try {
                this.twain32.CloseDataSource();
                this.twain32.SourceIndex = this.dsToolStripComboBox.SelectedIndex;
                this._LoadCaps();
            } catch(Exception ex) {
                this._ToLog(ex);
            }
        }

        private void _DeviceSelectedIndexChanged(object sender, EventArgs e) {
            try {
                this.twain32.SetCap(CustomCap.VideoDevices, (this.deviceToolStripComboBox.SelectedItem as string).PadRight(255, '\0'));
                this._LoadFrameSizes();
            } catch(Exception ex) {
                this._ToLog(ex);
            }
        }

        private void _AcquireClick(object sender, EventArgs e) {
            try {
                this.twain32.Acquire();
            } catch(Exception ex) {
                this._ToLog(ex);
            }
        }

        private void _SaveClick(object sender, EventArgs e) {
            try {
                if(this.saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    this.pictureBox1.Image.Save(this.saveFileDialog1.FileName, ImageFormat.Jpeg);
                }
            } catch(Exception ex) {
                this._ToLog(ex);
            }
        }

        private void _ToLog(Exception ex) {
        }

        private void _AcquireError(object sender, Twain32.AcquireErrorEventArgs e) => this._ToLog(e.Exception);

        private void _EndXfer(object sender, Twain32.EndXferEventArgs e) {
            try {
                if(this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => {
                        this.pictureBox1.Image?.Dispose();
                        this.saveToolStripButton.Enabled = (this.pictureBox1.Image = e.Image) != null;
                    }));
                } else {
                    this.pictureBox1.Image?.Dispose();
                    this.saveToolStripButton.Enabled = (this.pictureBox1.Image = e.Image) != null;
                }
            } catch(Exception ex) {
                this._ToLog(ex);
            }
        }

        private static class CustomCap {
            public const TwCap VideoDevices = (TwCap)0x9001;
            public const TwCap FrameWidth = (TwCap)0x9002;
            public const TwCap FrameHeight = (TwCap)0x9003;
            public const TwCap FrameBpp = (TwCap)0x9004;
            public const TwCap IsRunning = (TwCap)0x9005;
        }
    }
}
