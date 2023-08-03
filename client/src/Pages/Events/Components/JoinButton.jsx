import PrimaryButton from '../../../Components/Button/PrimaryButton'

export default function JoinButton({eventId, setEvents}) {
    console.log(eventId)

    const eventJoinHandler = (id) => {
        fetch(`https://localhost:7019/EventEndpoints/join-event/${id}`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
        .then(res=>{
            if(res.ok){
                setEvents(events=>events.filter(e=>e.id!==id))
            }
            return res.json()
        })
        .then(data=>{
            console.log(data)
        })
        .catch(console.log)
    }
  return <PrimaryButton text='Join event' clickHandler={()=>eventJoinHandler(eventId)}/>
  
}
