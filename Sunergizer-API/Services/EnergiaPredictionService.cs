using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;

namespace Sunergizer_API.Services
{
    public class EnergiaPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public EnergiaPredictionService()
        {
            _mlContext = new MLContext();

            // Dados de exemplo para treinar o modelo
            var trainingData = new List<EnergiaData>
            {
                new EnergiaData { KwhConsumidos = 50f, Sugestao = "Consumo baixo, sem recomendações" },
                new EnergiaData { KwhConsumidos = 200f, Sugestao = "Considere reduzir o uso de eletrodomésticos" },
                new EnergiaData { KwhConsumidos = 500f, Sugestao = "Avalie opções de energia renovável" }
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Pipeline de treinamento
            var pipeline = _mlContext.Transforms.CopyColumns("Label", nameof(EnergiaData.KwhConsumidos)) // Define a coluna de rótulo como 'KwhConsumidos'
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(EnergiaData.Sugestao))) // Featuriza a sugestão
                .Append(_mlContext.Regression.Trainers.Sdca());

            // Treina o modelo
            _model = pipeline.Fit(dataView);
        }

        public string Predict(float kwhConsumidos)
        {
            // Dados de entrada para previsão
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<EnergiaData, EnergiaPrediction>(_model);

            var input = new EnergiaData { KwhConsumidos = kwhConsumidos };
            var prediction = predictionEngine.Predict(input);

            // Retorna uma sugestão com base no valor previsto
            if (kwhConsumidos < 100)
                return "Consumo eficiente, mantenha o bom trabalho!";
            if (kwhConsumidos < 300)
                return "Consumo médio, considere otimizar o uso.";
            return "Consumo alto, avalie alternativas para redução.";
        }
    }

    public class EnergiaData
    {
        [LoadColumn(0)]
        public float KwhConsumidos { get; set; }  // Alterado para 'float'
        [LoadColumn(1)]
        public string Sugestao { get; set; }
    }

    public class EnergiaPrediction
    {
        public float Score { get; set; }
    }
}
