using System.Collections.Generic;
using System.Windows.Forms;
using IngSoft.Abstractions;

namespace IngSoft.Services
{
    public static class MultiIdiomaManager
    {
        private static IIdioma _idioma;

        public static IIdioma Idioma
        {
            get => _idioma;
        }

        public static void CambiarIdiomaControles(Form thisForm, List<IControlIdioma> controlIdiomas)
        {
            foreach (Control control in thisForm.Controls)
            {
                foreach (var idioma in controlIdiomas)
                {
                    if (control.Name == idioma.NombreControl)
                    {
                        control.Text = idioma.TextoTraducido;
                    }
                }
            }
        }

        public static void SetIdioma(IIdioma idioma)
        {
            _idioma = idioma;
        }
    }
}
