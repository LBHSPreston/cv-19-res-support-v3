using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateResidentAndHelpRequestUseCase
    {
        int Execute(CreateResidentAndHelpRequest command);
    }
}
