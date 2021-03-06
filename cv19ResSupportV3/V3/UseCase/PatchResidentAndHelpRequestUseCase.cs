using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public class PatchResidentAndHelpRequestUseCase : IPatchResidentAndHelpRequestUseCase
    {
        private readonly IPatchResidentUseCase _patchResidentUseCase;
        private readonly IPatchHelpRequestUseCase _patchHelpRequestUseCase;
        private readonly IPatchCaseNoteUseCase _patchCaseNoteUseCase;
        public PatchResidentAndHelpRequestUseCase(IPatchResidentUseCase patchResidentUseCase,
            IPatchHelpRequestUseCase patchHelpRequestUseCase, IPatchCaseNoteUseCase patchCaseNoteUseCase)
        {
            _patchResidentUseCase = patchResidentUseCase;
            _patchHelpRequestUseCase = patchHelpRequestUseCase;
            _patchCaseNoteUseCase = patchCaseNoteUseCase;
        }

        public void Execute(int id, PatchResidentAndHelpRequest command)
        {
            var helpRequest = _patchHelpRequestUseCase.Execute(id, command.ToPatchHelpRequestCommand());
            _patchResidentUseCase.Execute(helpRequest.ResidentId, command.ToPatchResidentCommand());
            if (command.CaseNotes != null)
            {
                _patchCaseNoteUseCase.Execute(id, helpRequest.ResidentId, command.CaseNotes);
            }
        }
    }
}
