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
using System.IO;
using System.Linq;
using System.Text;
using Saraff.Twain.DS.Extensions;

namespace Saraff.Twain.DS.DirectX.Core {

    internal sealed class _LogService : Component, ILog {

        public void Write(Exception ex) {
            var _msg = string.Empty;
            for(var _ex = ex; _ex != null; _ex = _ex.InnerException) {
                _msg += $"UserName = {Environment.UserName}; ProcessName = {System.Diagnostics.Process.GetCurrentProcess().ProcessName}{Environment.NewLine}";
                _msg += $"{_ex.GetType().Name}: {ex.Message}{Environment.NewLine}";
                if(_ex is DataSourceException _ex2) {
                    _msg += $"ReturnCode = {_ex2.ReturnCode}; ConditionCode = {_ex2.ConditionCode};{Environment.NewLine}";
                }
                _msg += $"{ex.StackTrace}{Environment.NewLine}{Environment.NewLine}";
            }
            this.Write(_msg, LogLevel.Error);
        }

        public void Write(string message, LogLevel level = LogLevel.None) {
            try {
                var _dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), typeof(MediaDataSource).GUID.ToString());
                Directory.CreateDirectory(_dir);

                var _file = $"{DateTime.Now.ToString("O")}-{level.ToString()}.txt";
                foreach(var i in Path.GetInvalidFileNameChars()) {
                    _file = _file.Replace(i, '.');
                }
                File.WriteAllText(Path.Combine(_dir, _file), message);
            } catch { }
        }
    }
}
