using cv19ResRupportV3.V3.Boundary.Response;
using cv19ResRupportV3.V3.Domain;
using cv19ResRupportV3.V3.Factories;
using cv19ResRupportV3.V3.Gateways;
using cv19ResRupportV3.V3.UseCase.Interfaces;

namespace cv19ResRupportV3.V3.UseCase
{
    public class CreateHelpRequestUseCase : ICreateHelpRequestUseCase
    {
        private IHelpRequestGateway _gateway;
        public CreateHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        //TODO: rename id to the name of the identifier that will be used for this API, the type may also need to change
        public HelpRequestResponse Execute(HelpRequest request)
        {
            var response =  _gateway.CreateHelpRequest(request.ToEntity());
            return new HelpRequestResponse
            {
                Id = response
            };
        }
    }
}
