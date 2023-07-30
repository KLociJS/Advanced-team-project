import './DateInput.css'

export default function DateInput({ dateValue,setDateValue, time, setTime, label }) {
  
  const handleDateChange = (e) => {
    const selectedDate = e.target.value
    const formattedDate = new Date(selectedDate).toISOString().slice(0, 10)
    setDateValue(selectedDate)
  }
  const handleTimeChange = (e) =>{
    setTime(e.target.value)
  }

  return (
    <div className='input-group date-input'>
        <label className='input-label active'>
            {label}
        </label>
        <input
            type='date'
            value={dateValue}
            className="input-control"
            onChange={handleDateChange}
            autoComplete="off"
        />
        <input
          type="time"
          value={time}
          onChange={handleTimeChange}
          className="date-input-control"
        />
        
    </div>
  );
}
