using Microsoft.ML;
using Sunergizer_API.ML;

namespace Sunergizer_API.Services
{
    public class MLModelTrainer
    {
        private const string ModelPath = "MLModels/energia_model.zip";

        public void TrainAndSaveModel()
        {
            var mlContext = new MLContext();

            // Dados de treinamento
            var trainingData = new List<EnergiaModel>
            {
                new EnergiaModel { KwhConsumidos = 50, Sugestao = "Consumo eficiente! Continue assim." },
                new EnergiaModel { KwhConsumidos = 300, Sugestao = "Pense em substituir equipamentos por modelos mais eficientes." },
                new EnergiaModel { KwhConsumidos = 700, Sugestao = "Considere instalar painéis solares para reduzir custos." }
            };

            // Conversão para IDataView
            var trainingDataView = mlContext.Data.LoadFromEnumerable(trainingData);

            // Definir pipeline
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(EnergiaModel.KwhConsumidos))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(EnergiaModel.Sugestao)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Treinar modelo
            var model = pipeline.Fit(trainingDataView);

            // Salvar modelo
            mlContext.Model.Save(model, trainingDataView.Schema, ModelPath);
        }
    }
}
