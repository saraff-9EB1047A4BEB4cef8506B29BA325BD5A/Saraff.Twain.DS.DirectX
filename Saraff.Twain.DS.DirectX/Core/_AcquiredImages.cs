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
using System.Linq;
using System.Text;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX.Core {

    internal sealed class _AcquiredImages : Component, IAcquiredImages {
        private Dictionary<Guid, Bitmap> _images = new Dictionary<Guid, Bitmap>();
        private Dictionary<Guid, ImageTagInfo> _tags = new Dictionary<Guid, ImageTagInfo>();

        #region IAcquiredImages

        public Guid Add(Bitmap image) {
            var _id = Guid.NewGuid();
            this._tags.Add(_id, new ImageTagInfo { Flags = ImageFlags.None });
            this._images.Add(_id, image);
            return _id;
        }

        public void Remove(Guid guid) {
            this._tags.Remove(guid);
            this._images.Remove(guid);
        }

        public ImageTagInfo GetTagInfo(Guid guid) => this._tags[guid];

        public void SetTagInfo(Guid guid, ImageTagInfo tag) => this._tags.Add(guid, tag);

        public IEnumerable<Bitmap> Get() => this._images.Keys
            .Where(x => !this._tags.ContainsKey(x) || this._tags.ContainsKey(x) && !this._tags[x].Flags.HasFlag(ImageFlags.Hidden))
            .Select(x => this._images[x]);

        public Bitmap Get(Guid guid) => this._images[guid];

        public void Clear() {
            foreach(var _key in this._images.Keys
                .Where(x => !this._tags.ContainsKey(x) || this._tags.ContainsKey(x) && !this._tags[x].Flags.HasFlag(ImageFlags.Hidden))
                .ToList()) {

                this.Remove(_key);
            }
        }

        #endregion

        protected override void Dispose(bool disposing) {
            this._images.Values.Dispose();
            base.Dispose(disposing);
        }
    }
}
