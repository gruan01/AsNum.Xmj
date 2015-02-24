using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AsNum.Common {
    public static class MefHelper {
        public static AggregateCatalog SafeDirectoryCatalog(string directory) {
            var files = Directory.EnumerateFiles(directory, "*.dll", SearchOption.AllDirectories);

            var catalog = new AggregateCatalog();

            foreach(var file in files) {
                try {
                    var asmCat = new AssemblyCatalog(file);

                    //Force MEF to load the plugin and figure out if there are any exports
                    // good assemblies will not throw the RTLE exception and can be added to the catalog
                    if (asmCat.Parts.ToList().Count > 0)
                        catalog.Catalogs.Add(asmCat);
                } catch (ReflectionTypeLoadException) {
                } catch (BadImageFormatException) {
                } catch (Exception) {
                }
                //ResolutionFailedException
            }

            return catalog;
        }

        public static void ComposeParts(object obj) {
            //AggregateCatalog catalog = new AggregateCatalog(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            CompositionContainer container = new CompositionContainer(MefHelper.SafeDirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            try {
                container.ComposeParts(obj);
            } catch {

            }
        }
    }
}
