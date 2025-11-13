namespace Integração_BrasilInDoc.BrasilInDoc.Domain
{
    public enum ApplicationType : int
    {
        Client = 1,
        Person = 2,
        Company = 3
    }

    public enum DocumentType : int
    {
        RG = 1,
        CNH = 2,
        RG_Profissional = 3,
        DNI = 4
    }

    public enum FormalizationType : int
    {
        Digital = 1,
        Physical = 2
    }

    public enum StatusType : int
    {
        New = 1,
        In_Analysis = 2,
        Return_to_Requester = 3,
        Pending = 4,
        Approved = 5,
        Rejected = 6,
        Canceled = 7,
        Finalized_Approved = 8,
        Finalized_Rejected = 9
    }

}
