using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ITimeslots
    {
        Response GetAllTimeslots();
        Response GetTimeslotByID(string timeslotID);
        Response AddTimeslot(GetTimeslotModal addTimeslot);
        Response UpdateTimeslot(GetTimeslotModal updateTimeslot);
        Response DeleteTimeslot(GetTimeslotModal deleteTimeslot);
        Response ActivateTimeslot(GetTimeslotModal activateTimeslot);
    }
}
