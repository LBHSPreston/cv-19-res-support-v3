using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface ICaseNotesGateway
    {
        ResidentCaseNote PatchCaseNote(int helpRequestId, int residentId, string caseNote);
        ResidentCaseNote CreateCaseNote(int residentId, int helpRequestId, string caseNote);
        ResidentCaseNote UpdateCaseNote(int helpRequestId, int residentId, string caseNote);
        List<ResidentCaseNote> GetByResidentId(int residentId);
        List<ResidentCaseNote> GetByHelpRequestId(int helpRequestId);
    }
}
