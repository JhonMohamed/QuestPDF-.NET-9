using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure
{
    public static class IconHelper
    {
        /// <summary>
        /// Lee un SVG incrustado y devuelve su contenido XML como string.
        /// </summary>
        public static string LoadSvgContent(string iconName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourceName = asm.GetManifestResourceNames()
                .FirstOrDefault(x => x.EndsWith($".Icons.{iconName}.svg", StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new FileNotFoundException($"No se encontró el recurso incrustado: {iconName}.svg");

            using var stream = asm.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
