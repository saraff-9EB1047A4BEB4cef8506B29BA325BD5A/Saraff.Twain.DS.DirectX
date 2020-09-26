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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Saraff.Twain.DS;
using Saraff.Twain.DS.DirectX;
using Saraff.Twain.DS.DirectX.ComponentModel;
using Extensions = Saraff.Twain.DS.Extensions;
using IoC = Saraff.Twain.DS.IoC;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Saraff.Twain.DS.DirectX")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("SARAFF SOFTWARE")]
[assembly: AssemblyProduct("Saraff DirectX DS")]
[assembly: AssemblyCopyright("Copyright © SARAFF SOFTWARE 2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("89d06c4f-7eca-45ca-8942-ef5a448f25d3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.4.717")]
[assembly: AssemblyFileVersion("1.0.4.717")]

[assembly: DataSource(typeof(MediaDataSource), MaxConnectionCount = 1)]
[assembly: IoC.BindService(typeof(Extensions.ILog), typeof(Saraff.Twain.DS.DirectX.Core._LogService))]
[assembly: IoC.BindService(typeof(IVideoDevices), typeof(Saraff.Twain.DS.DirectX.Core._VideoDevices))]
[assembly: IoC.BindService(typeof(IAcquiredImages), typeof(Saraff.Twain.DS.DirectX.Core._AcquiredImages))]
[assembly: IoC.BindService(typeof(IPersistent), typeof(Saraff.Twain.DS.DirectX.Core._PersistentService))]
