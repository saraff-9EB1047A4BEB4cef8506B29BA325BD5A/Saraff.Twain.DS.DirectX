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
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using Microsoft.Win32;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX.Core {

    internal sealed class _PersistentService : Component, IPersistent {
        private const string _keyName = "Software\\SARAFF\\E44C2DC8-363D-4DCB-8EF1-0FFD72B54673";
        private RegistryKey _key = null;

        #region IPersistent

        public bool IsTransferImmediately {
            get => (int)this.RegistryKey.GetValue(nameof(_PersistentService.IsTransferImmediately), 0) == 1;
            set => this.RegistryKey.SetValue(nameof(_PersistentService.IsTransferImmediately), value ? 1 : 0, RegistryValueKind.DWord);
        }

        public string SourceMonikerString {
            get => this.RegistryKey.GetValue(nameof(_PersistentService.SourceMonikerString)) as string;
            set => this.RegistryKey.SetValue(nameof(_PersistentService.SourceMonikerString), value);
        }

        public Size SourceSnapshotResolution {
            get {
                using(var _stream = new MemoryStream(this.RegistryKey.GetValue(nameof(_PersistentService.SourceSnapshotResolution), new byte[0]) as byte[])) {
                    if(_stream.Length == 0) {
                        return Size.Empty;
                    }
                    return (Size)new SoapFormatter().Deserialize(_stream);
                }
            }
            set {
                using(var _stream = new MemoryStream()) {
                    new SoapFormatter().Serialize(_stream, value);
                    this.RegistryKey.SetValue(nameof(_PersistentService.SourceSnapshotResolution), _stream.ToArray(), RegistryValueKind.Binary);
                }
            }
        }

        public RotateFlipType RotateFlipType {
            get => (RotateFlipType)(int)this.RegistryKey.GetValue(nameof(_PersistentService.RotateFlipType), RotateFlipType.RotateNoneFlipNone);
            set => this.RegistryKey.SetValue(nameof(_PersistentService.RotateFlipType), (int)value, RegistryValueKind.DWord);
        }

        #endregion

        protected override void Dispose(bool disposing) {
            if(disposing) {
                this._key?.Dispose();
                this._key = null;
            }
            base.Dispose(disposing);
        }

        private RegistryKey RegistryKey => this._key ?? (this._key = Registry.CurrentUser.CreateSubKey(_PersistentService._keyName, RegistryKeyPermissionCheck.ReadWriteSubTree));
    }
}
