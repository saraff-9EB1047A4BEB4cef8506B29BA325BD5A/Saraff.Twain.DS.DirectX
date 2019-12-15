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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Saraff.Twain.DS.Capabilities;
using Saraff.Twain.DS.DirectX.ComponentModel;

namespace Saraff.Twain.DS.DirectX.Capabilities {

    [DataSourceCapability((TwCap)0x9005, TwType.Bool, SupportedOperations = TwQC.Get | TwQC.GetCurrent | TwQC.GetDefault | TwQC.Set, Get = TwOn.Enum)]
    internal sealed class IsRunningDataSourceCapability : EnumDataSourceCapability<bool> {

        #region EnumDataSourceCapability

        protected override Collection<bool> CoreValues => new Collection<bool> { false, true };

        protected override int CurrentIndexCore {
            get => this.Devices.Current.IsRunning ? 1 : 0;
            set {
                var _device = this.Devices.Current;
                switch(value) {
                    case 0:
                        _device.SignalToStop();
                        _device.WaitForStop();
                        break;
                    case 1:
                        _device.Start();
                        break;
                }
            }
        }

        protected override int DefaultIndexCore => 0;

        #endregion

        [IoC.ServiceRequired]
        public IVideoDevices Devices { get; set; }
    }
}
