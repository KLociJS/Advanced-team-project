export default function DateInput({ inputValue,setInputValue, label }) {
  
  const handleChange = (e) => {
    const selectedDate = e.target.value
    const formattedDate = new Date(selectedDate).toISOString().slice(0, 10)
    setInputValue(formattedDate)
  }

  return (
    <div className='input-group'>
        <label className='input-label active'>
            {label}
        </label>
        <input
            type='date'
            value={inputValue}
            className="input-control"
            onChange={handleChange}
            autoComplete="off"
        />
    </div>
  );
}
