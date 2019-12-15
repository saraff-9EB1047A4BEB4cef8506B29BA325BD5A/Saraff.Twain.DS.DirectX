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
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Saraff.Twain.DS.BitmapSource;
using Saraff.Twain.DS.Capabilities;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX {

    [Guid("E44C2DC8-363D-4DCB-8EF1-0FFD72B54673")]
    //[Compression(/*TwCompression.Jpeg*//*,TwCompression.Png, ... */)] //ICAP_COMPRESSION (CompressionAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    //[LsbFirstSupported] //ICAP_BITORDER (LsbFirstSupportedAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    [DeviceProperties(
        1280f, //ICAP_PHYSICALWIDTH (DevicePropertiesAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
        1024f, //ICAP_PHYSICALHEIGHT (DevicePropertiesAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
        96f, //ICAP_XNATIVERESOLUTION (DevicePropertiesAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
        96f)] //ICAP_YNATIVERESOLUTION (DevicePropertiesAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    [PlanarChunky(TwPC.Chunky, DefaultValue=TwPC.Chunky)] //ICAP_PLANARCHUNKY (PlanarChunkyAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    [PixelFlavor(TwPF.Chocolate)] //ICAP_PIXELFLAVOR (PixelFlavorAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    [PixelType(TwPixelType.RGB, DefaultValue=TwPixelType.RGB)] //ICAP_PIXELTYPE (PixelTypeAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    #region When ICAP_PIXELTYPE supported TWPT_BW (TwPixelType.BW) value
    //[BitDepthReduction(TwBR.Threshold, TwBR.Diffusion)] //ICAP_BITDEPTHREDUCTION (BitDepthReductionAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    //[Halftones("A1","A2","A3")] //ICAP_HALFTONES (HalftonesAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    //[CustHalftone(...)] //ICAP_CUSTHALFTONE (CustHalftoneAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    //[Threshold(128f)] //ICAP_THRESHOLD (ThresholdAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    #endregion
    [XferMech(File=true)/*ICAP_XFERMECH*/, MemXferBufferSize(64*1024U /*64K*/)/*TW_SETUPMEMXFER.Preferred on DG_CONTROL / DAT_SETUPMEMXFER / MSG_GET operation*/]
    [ImageFileFormat(TwFF.Bmp, TwFF.Tiff, TwFF.Jfif)] //ICAP_IMAGEFILEFORMAT (ImageFileFormatAttribute or DataSource.OnCapabilityValueNeeded(CapabilityEventArgs))
    [Capability(typeof(Capabilities.BitDepthDataSourceCapability))]
    [Capability(typeof(Capabilities.XResolutionDataSourceCapability))]
    [Capability(typeof(Capabilities.YResolutionDataSourceCapability))]
    [Capability(typeof(Capabilities.DeviceOnlineDataSourceCapability))]
    [Capability(typeof(Capabilities.VideoDevicesDataSourceCapability))]
    [Capability(typeof(Capabilities.FrameWidthDataSourceCapability))]
    [Capability(typeof(Capabilities.FrameHeightDataSourceCapability))]
    [Capability(typeof(Capabilities.FrameBppDataSourceCapability))]
    [Capability(typeof(Capabilities.IsRunningDataSourceCapability))]
    [Capability(typeof(Capabilities.CustomInterfaceGuidDataSourceCapability))]
    public sealed class MediaDataSource:BitmapDataSource {
        private readonly float _resolution=96f;
        private UI.DataSourceForm _form;
        private Queue<Bitmap> _images = null;

        protected override RectangleF CurrentImageLayout {
            get {
                if(base.CurrentImageLayout == RectangleF.Empty) {
                    base.CurrentImageLayout = this.DefaultImageLayout;
                }
                return base.CurrentImageLayout;
            }
            set {
                if(value.Right > this.DefaultImageLayout.Right || value.Bottom > this.DefaultImageLayout.Bottom) {
                    throw new ArgumentException();
                }
                base.CurrentImageLayout = value;
            }
        }

        protected override RectangleF DefaultImageLayout {
            get {
                var _device = this.VideoDevices.Current;
                SizeF _size = (_device.SnapshotResolution ?? _device.SnapshotCapabilities.First()).FrameSize;
                return new RectangleF(PointF.Empty, new SizeF(_size.Width / this._resolution, _size.Height / this._resolution));
            }
        }

        protected override void OnEnableDS(bool showUI, bool modalUI, IntPtr hwnd) {
            if(showUI) {
                this._form = this.Factory.CreateInstance<UI.DataSourceForm>();
                this._form.DoneCallback += (sender, e) => this.OnXferReady();
                this._form.Show();
            } else {
                this.VideoDevices.SnapshotFrame += this._FrameHandler;
                this.VideoDevices?.Current?.Start();
            }

            base.OnEnableDS(showUI, modalUI, hwnd);
        }

        protected override void OnDisableDS(IntPtr hwnd) {
            this._form?.Close();
            this._form?.Dispose();
            this._form = null;

            this.VideoDevices.SnapshotFrame -= this._FrameHandler;

            var _device = this.VideoDevices.Current;
            if(_device?.IsRunning ?? false) {
                _device.SignalToStop();
            }

            this._images?.Dispose();
            this._images = null;

            base.OnDisableDS(hwnd);
        }

        /// <summary>
        /// Invoked to indicate that the Source has data that is ready to be transferred.
        /// </summary>
        protected override void OnXferReady() {
            this._images?.Dispose();
            this._images = new Queue<Bitmap>(this.AcquiredImages.Get());

            this.XferEnvironment.PendingXfers = (ushort)this._images.Count;

            base.OnXferReady();
        }

        protected override Bitmap Acquire() => this._images.Dequeue();

        private void _FrameHandler(object sender, AForge.Video.NewFrameEventArgs e) {
            this.AcquiredImages?.Add(e.Frame.Clone() as Bitmap);
            if(this.AcquiredImages?.Get().Count() >= Math.Abs((short)this[TwCap.XferCount].Value)) {
                this.OnXferReady();
                this.AcquiredImages.Clear();
            }
        }

        [IoC.ServiceRequired]
        public Extensions.ILog Log { get; set; }

        [IoC.ServiceRequired]
        public IVideoDevices VideoDevices { get; set; }

        [IoC.ServiceRequired]
        public IAcquiredImages AcquiredImages { get; set; }
    }
}
