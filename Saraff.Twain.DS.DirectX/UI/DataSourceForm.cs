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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX.UI {

    internal sealed partial class DataSourceForm : Form {

        public DataSourceForm() {
            this.InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            try {
                this.rotateFlipTypeViewBindingSource.DataSource =
                    new RotateFlipType[] {
                        RotateFlipType.RotateNoneFlipNone,
                        RotateFlipType.Rotate90FlipNone,
                        RotateFlipType.Rotate180FlipNone,
                        RotateFlipType.Rotate270FlipNone,
                        RotateFlipType.RotateNoneFlipX,
                        RotateFlipType.Rotate90FlipX,
                        RotateFlipType.RotateNoneFlipY,
                        RotateFlipType.Rotate90FlipY }
                    .Select(x => this.Factory.CreateInstance<RotateFlipTypeView>(i => i("value", x)))
                    .ToList()
                    .AsReadOnly();
                var _rotateId = this.PersistentService?.RotateFlipType ?? RotateFlipType.RotateNoneFlipNone;
                this.rotateFlipTypeViewBindingSource.Position = this.rotateFlipTypeViewBindingSource.Cast<RotateFlipTypeView>()
                    .Select((x, i) => new { Value = x, Index = i })
                    .FirstOrDefault(x => x.Value.RotateFlipType == _rotateId).Index;
                this.rotateFlipTypeViewBindingSource.CurrentChanged += this._RotateFlipTypeViewBindingSourceCurrentChanged;

                this.filterInfoViewBindingSource.DataSource = this.VideoDevices()?.Get()
                    .Select(x => this.Factory.CreateInstance<FilterInfoView>(i => i("device", x)))
                    .ToList()
                    .AsReadOnly();
                this.filterInfoViewBindingSource.Position = this.VideoDevices()?.Position ?? this.filterInfoViewBindingSource.Position;
                this.filterInfoViewBindingSource.CurrentChanged += this._FilterInfoViewBindingSourceCurrentChanged;

                this.VideoDevices().SnapshotFrame += this._FrameHandler;
                this._Connect();
                this._IsTransferImmediately = this.PersistentService?.IsTransferImmediately ?? false;
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            try {
                this.VideoDevices().SnapshotFrame -= this._FrameHandler;

                if(this.PersistentService != null) {
                    this.PersistentService.IsTransferImmediately = this._IsTransferImmediately;
                    this.PersistentService.SourceSnapshotResolution = this._CurrentShapshotView?.Value.FrameSize ?? Size.Empty;
                    this.PersistentService.RotateFlipType = this._CurrentRotateFlipTypeView.RotateFlipType;
                }
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void OnDoneCallback(EventArgs e) {
            this.DoneCallback?.Invoke(this, e);

            foreach(var _item in this.flowLayoutPanel1.Controls
                .Cast<ThumbnailControl>()
                .Where(x => x.ImageTag == null || !x.ImageTag.Flags.HasFlag(ImageFlags.Hidden))
                .ToList()) {

                _item.Remove();
            }
        }

        private void _FrameHandler(object sender, AForge.Video.NewFrameEventArgs e) => this.Invoke(new MethodInvoker(() => {
            try {
                var _image = e.Frame.Clone() as Bitmap;
                _image.RotateFlip(this._CurrentRotateFlipTypeView.RotateFlipType);
                this._AddThumbnail(this.AcquiredImages.Add(_image));
                if(this._IsTransferImmediately) {
                    this.OnDoneCallback(EventArgs.Empty);
                }
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }));

        private  void _AddThumbnail(Guid guid) {
            var _control = this.Factory.CreateInstance<ThumbnailControl>(i => i("guid", guid));
            this.flowLayoutPanel1.Controls.Add(_control);
            this.flowLayoutPanel1.ScrollControlIntoView(_control);
        }

        private void _Connect() {
            var _device = this._CurrentFilterInfoView?.VideoDevice;
            if(_device != null) {

                #region Snapshot

                this.snapshotBindingSource.DataSource = this.VideoDevices().SnapshotCapabilities
                    .Select(x => this.Factory.CreateInstance<CapabilityView>(i => i("value", x)))
                    .ToList();

                if(this.VideoDevices().SnapshotResolution == null) {
                    var _size = this.PersistentService?.SourceSnapshotResolution;
                    this.VideoDevices().SnapshotResolution = this.VideoDevices().SnapshotCapabilities.FirstOrDefault(x => x.FrameSize == _size) ?? this.VideoDevices().SnapshotCapabilities.FirstOrDefault();
                }
                this.snapshotBindingSource.Position = this.VideoDevices().SnapshotCapabilities
                    .Select((x, i) => new { Element = x, Index = i })
                    .FirstOrDefault(x => x.Element.FrameSize == this.VideoDevices().SnapshotResolution.FrameSize && x.Element.BitCount == this.VideoDevices().SnapshotResolution.BitCount)?.Index ?? 0;

                this.snapshotBindingSource.CurrentChanged += this._SnapshotBindingSourceCurrentChanged;

                #endregion

                #region Video

                this.videoBindingSource.DataSource = this.VideoDevices().VideoCapabilities
                   .Select(x => this.Factory.CreateInstance<CapabilityView>(i => i("value", x)))
                   .ToList();

                if(this.VideoDevices().VideoResolution == null) {
                    var _size = this.PersistentService?.SourceVideoResolution;
                    this.VideoDevices().VideoResolution = this.VideoDevices().VideoCapabilities.FirstOrDefault(x => x.FrameSize == _size) ?? this.VideoDevices().VideoCapabilities.FirstOrDefault();
                }
                this.videoBindingSource.Position = this.VideoDevices().VideoCapabilities
                    .Select((x, i) => new { Element = x, Index = i })
                    .FirstOrDefault(x => x.Element.FrameSize == this.VideoDevices().VideoResolution.FrameSize && x.Element.BitCount == this.VideoDevices().VideoResolution.BitCount)?.Index ?? 0;

                this.videoBindingSource.CurrentChanged += this._VideoBindingSourceCurrentChanged;

                #endregion

                this._Resize();

                this.player.VideoSource = _device;
                this.player.Start();
            }
        }

        private void _Disconnect() {
            this.snapshotBindingSource.CurrentChanged -= this._SnapshotBindingSourceCurrentChanged;
            if(this.player.VideoSource != null) {
                this.player.SignalToStop();
                this.player.WaitForStop();
                this.player.VideoSource = null;
            }
        }

        private void _Resize() {
            var _frame = this._CurrentVideoView.Value.FrameSize;

            var _area = Screen.PrimaryScreen.WorkingArea.Size;
            var _width = this.Width - this.player.Width + (this._CurrentRotateFlipTypeView.IsToSide ? _frame.Height : _frame.Width);
            var _height = this.Height - this.player.Height + (this._CurrentRotateFlipTypeView.IsToSide ? _frame.Width : _frame.Height);

            if(_width < _area.Width && _height < _area.Height) {
                this.Width = _width;
                this.Height = _height;
            } else {
                this.Location = Point.Empty;
                this.Width = _width * Math.Min(_area.Height, _height * Math.Min(_area.Width, _width) / _width) / _height;
                this.Height = _height * Math.Min(_area.Width, _width * Math.Min(_area.Height, _height) / _height) / _width;
            }
        }

        #region Properties

        private bool _IsTransferImmediately {
            get => this.transferImmediatelyCheckBox.Checked;
            set => this.transferImmediatelyCheckBox.Checked = value;
        }

        private FilterInfoView _CurrentFilterInfoView => this.filterInfoViewBindingSource.Current as FilterInfoView;

        private CapabilityView _CurrentShapshotView => this.snapshotBindingSource.Current as CapabilityView;

        private CapabilityView _CurrentVideoView => this.videoBindingSource.Current as CapabilityView;

        private RotateFlipTypeView _CurrentRotateFlipTypeView => this.rotateFlipTypeViewBindingSource.Current as RotateFlipTypeView;

        [IoC.ServiceRequired]
        public IoC.Lazy<IVideoDevices> VideoDevices { get; set; }

        [IoC.ServiceRequired]
        public IAcquiredImages AcquiredImages { get; set; }

        [IoC.ServiceRequired]
        public Extensions.ILog Log { get; set; }

        [IoC.ServiceRequired]
        public IoC.IInstanceFactory Factory { get; set; }

        [IoC.ServiceRequired]
        public IPersistent PersistentService { get; set; }

        #endregion

        #region Events Handlers

        private void _FilterInfoViewBindingSourceCurrentChanged(object sender, EventArgs e) {
            try {
                this._Disconnect();
                this.VideoDevices().Position = this.filterInfoViewBindingSource.Position;
                this._Connect();
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _SnapshotBindingSourceCurrentChanged(object sender, EventArgs e) {
            try {
                var _cap = this._CurrentShapshotView?.Value;
                var _device = this._CurrentFilterInfoView?.VideoDevice;
                if(_device != null && _cap != null) {
                    _device.SignalToStop();
                    _device.WaitForStop();
                    _device.SnapshotResolution = _cap;

                    _device.Start();
                }
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _VideoBindingSourceCurrentChanged(object sender, EventArgs e) {
            try {
                var _cap = this._CurrentVideoView?.Value;
                var _device = this._CurrentFilterInfoView?.VideoDevice;
                if(_device != null && _cap != null) {
                    _device.SignalToStop();
                    _device.WaitForStop();
                    _device.VideoResolution = _cap;

                    this._Resize();

                    _device.Start();
                }
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _RotateFlipTypeViewBindingSourceCurrentChanged(object sender, EventArgs e) {
            try {
                this._Resize();
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _AcquireClick(object sender, EventArgs e) {
            try {
                this.VideoDevices().SimulateTrigger();
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _DoneClick(object sender, EventArgs e) {
            try {
                this.OnDoneCallback(EventArgs.Empty);
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        private void _PlayerNewFrame(object sender, ref Bitmap image) {
            try {
                image.RotateFlip(this._CurrentRotateFlipTypeView.RotateFlipType);
            } catch(Exception ex) {
                this.Log?.Write(ex);
            }
        }

        #endregion

        public event EventHandler DoneCallback;

        public sealed class FilterInfoView {

            [IoC.ServiceRequired]
            public FilterInfoView(VideoCaptureDevice device) {
                this.VideoDevice = device;
                this.FilterInfo = new FilterInfo(device.Source);
            }

            public VideoCaptureDevice VideoDevice {
                get; private set;
            }

            public FilterInfo FilterInfo {
                get; private set;
            }

            public string Name => this.FilterInfo.Name;
        }

        public sealed class CapabilityView {
            private VideoCapabilities _cap;
            
            [IoC.ServiceRequired]
            public CapabilityView(VideoCapabilities value) {
                this._cap = value;
            }

            public string Name => $"{this._cap.FrameSize.Width} x {this._cap.FrameSize.Height}, {this._cap.BitCount}bpp";

            public VideoCapabilities Value => this._cap;
        }

        public sealed class RotateFlipTypeView { 

            [IoC.ServiceRequired]
            public RotateFlipTypeView(RotateFlipType value) {
                this.RotateFlipType = value;
            }

            public RotateFlipType RotateFlipType { get; private set; }

            public string Name => this.RotateFlipType.ToString();

            public bool IsToSide => new RotateFlipType[] {
                RotateFlipType.Rotate90FlipNone,
                RotateFlipType.Rotate90FlipX,
                RotateFlipType.Rotate90FlipXY,
                RotateFlipType.Rotate90FlipY,
                RotateFlipType.Rotate270FlipNone,
                RotateFlipType.Rotate270FlipX,
                RotateFlipType.Rotate270FlipXY,
                RotateFlipType.Rotate270FlipY 
            }.Contains(this.RotateFlipType);
        }
    }
}
