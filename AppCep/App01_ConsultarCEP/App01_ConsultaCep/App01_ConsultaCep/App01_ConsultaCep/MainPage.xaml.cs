using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultaCep.Servico.Modelo;
using App01_ConsultaCep.Servico;

namespace App01_ConsultaCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //  Lógica do Programa.

            //  Validações.

            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {

                try
                {

                    Endereco end = ViaCEPServico.BuscarEnderecoViaCep(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereco: {0} - {1} | {2}, {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O CEP NÃO EXISTE", "OK");
                    };
                }
                catch (Exception)
                {
                    DisplayAlert("ERRO CRÍTICO", "Sem Acesso ao Servidor", "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido! O cep deve conter 8 caracteres.", "ok");
                valido = false;
            }
            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP))
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido! Digite apenas números.", "ok");
                valido = false;
            }


            return valido;
        }
    }
}
