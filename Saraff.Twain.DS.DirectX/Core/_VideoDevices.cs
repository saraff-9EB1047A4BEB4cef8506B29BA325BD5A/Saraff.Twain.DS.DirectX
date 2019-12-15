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
using AForge.Video;
using AForge.Video.DirectShow;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX.Core {

    internal sealed class _VideoDevices : Component, IVideoDevices {
        private List<VideoCaptureDevice> _devices = null;
        private int _current = -1;

        #region IVideoDevices

        public VideoCaptureDevice Current => this.Get()?.ElementAtOrDefault(this.Position);

        public int Position {
            get { 
                return this._current;
            }
            set {
                for(var _prev = this.Current; _prev != null; _prev = null) {
                    _prev.SnapshotFrame -= this._SnapshotFrameHandler;
                    _prev.SnapshotFrame -= this._NewFrameHandler;
                }

                this._current = value;

                var _val = this.Current;
                if(_val == null) {
                    throw new ArgumentOutOfRangeException();
                }
                _val.ProvideSnapshots = true;
                _val.SnapshotFrame += this._SnapshotFrameHandler;
                _val.NewFrame += this._NewFrameHandler;

                if(this.PersistentServise != null) {
                    this.PersistentServise.SourceMonikerString = _val.Source;
                }
            }
        }

        public List<VideoCaptureDevice> Get() {
            if(this._devices == null) {
                this._devices = new FilterInfoCollection(FilterCategory.VideoInputDevice)
                    .Cast<FilterInfo>()
                    .Select(x => new VideoCaptureDevice(x.MonikerString))
                    .Where(x => x.SnapshotCapabilities.Length > 0)
                    .ToList();
                if(this._devices.Count > 0) {

                    var _source = this.PersistentServise?.SourceMonikerString;
                    if(_source != null) {
                        this.Position = this._devices
                            .Select((x, i) => new { Element = x, Index = i })
                            .FirstOrDefault(x => x.Element.Source == _source)?.Index ?? 0;
                    } else {
                        this.Position = 0;
                    }
                }
            }
            return this._devices;
        }

        public event EventHandler<NewFrameEventArgs> NewFrame;

        public event EventHandler<NewFrameEventArgs> SnapshotFrame;

        #endregion

        [IoC.ServiceRequired]
        public IPersistent PersistentServise { get; set; }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if(this.Current?.IsRunning ?? false) {
                this.Current.SignalToStop();
            }
        }

        private void _NewFrameHandler(object sender, NewFrameEventArgs e) => this.NewFrame?.Invoke(this, e);

        private void _SnapshotFrameHandler(object sender, NewFrameEventArgs e) => this.SnapshotFrame?.Invoke(this, e);
    }
}
