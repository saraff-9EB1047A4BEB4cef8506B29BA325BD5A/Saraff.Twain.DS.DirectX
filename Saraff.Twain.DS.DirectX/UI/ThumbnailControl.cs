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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX.UI {

    internal sealed partial class ThumbnailControl : UserControl {
        private Guid _guid;

        public ThumbnailControl() {
            this.InitializeComponent();
        }

        [IoC.ServiceRequired]
        public ThumbnailControl(Guid guid, IAcquiredImages images) : this() {
            this._guid = guid;
            this.AcquiredImages = images;

            var _image = images.Get(guid);
            var _img = new Bitmap(_image.Width >> 2, _image.Height >> 2);
            using(var _gr = Graphics.FromImage(_img)) {
                _gr.DrawImage(_image, new Rectangle(0, 0, _img.Width, _img.Height), new Rectangle(0, 0, _image.Width, _image.Height), GraphicsUnit.Pixel);
            }
            this.pictureBox1.Image = this.Image = _img;
        }

        public Image Image { get; private set; }

        public ImageTagInfo ImageTag => this.AcquiredImages.GetTagInfo(this._guid);

        private IAcquiredImages AcquiredImages { get; set; }

        [IoC.ServiceRequired]
        public Extensions.ILog Log { get; set; }

        public void Remove() {
            this.AcquiredImages.Remove(this._guid);
            this.Parent.Controls.Remove(this);
        }

        private void _CheckedChanged(object sender, EventArgs e) {
            try {
                if(this.checkBox1.Checked) {
                    this.ImageTag.Flags &= ~ImageFlags.Hidden;
                } else {
                    this.ImageTag.Flags |= ImageFlags.Hidden;
                }
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _RemoveClick(object sender, EventArgs e) {
            try {
                var _img = this.AcquiredImages.Get(this._guid);
                this.Remove();
                _img.Dispose();
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }
    }
}
