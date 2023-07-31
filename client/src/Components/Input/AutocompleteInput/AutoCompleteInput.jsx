import { useState, useRef } from 'react'

const fetchSuggestions = async (url) =>{
    const suggestionsResponse = await fetch(url)
    return await suggestionsResponse.json()
}

export default function AutoCompleteInput({ inputValue, setInputValue, label, type, url }) {

    const [suggestions, setSuggestions] = useState([])
    const [isFocused, setIsFocused] = useState(false)

    const handleFocus = () => setIsFocused(true)
    const handleBlur = () => setIsFocused(false)
  
    const isActive = isFocused || inputValue ? 'active' : ''
    
    let debounceRef = useRef();

    const handleInputChange = async(e) => {
        const value = e.target.value
        setInputValue(value)

        if (value.length > 2) {
            if (debounceRef.current) {
                clearTimeout(debounceRef.current)
            }

            debounceRef.current = setTimeout(async () => {
                const currSuggestions = await fetchSuggestions(url + value);
                setSuggestions(currSuggestions.data)
            }, 300)
        }
    }
  
    return (
          <div className={`input-group`}>
              <label className={`input-label ${isActive}`}>
                  {label}
              </label>
              <input
                  type={type || 'text'}
                  value={inputValue}
                  className="input-control"
                  onFocus={handleFocus}
                  onBlur={handleBlur}
                  onChange={handleInputChange}
                  autoComplete="off"
                  list={label}
              />
              <datalist id={label}>
                {suggestions.map(s=>(
                    <option key={s.id} value={s.name}> {s.name} </option>
                ))}
              </datalist>
      </div>
    );
}
