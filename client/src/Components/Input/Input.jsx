import React, { useState } from 'react';
import './Input.css';

const Input = ({ 
  label, 
  inputValue, 
  setInputValue,
  hasError,
  type, 
}) => {
  const [isFocused, setIsFocused] = useState(false)

  const handleFocus = () => setIsFocused(true)
  const handleBlur = () => setIsFocused(false)
  const handleChange = (e) => {
    setInputValue(e.target.value)
  };

  const isActive = isFocused || inputValue ? 'active' : ''

  return (
        <div className={`input-group ${hasError ? 'error' : ''}`}>
            <label className={`input-label ${isActive}`}>
                {label}
            </label>
            <input
                type={type || 'text'}
                value={inputValue}
                className="input-control"
                onFocus={handleFocus}
                onBlur={handleBlur}
                onChange={handleChange}
                autoComplete="off"
            />
    </div>
  );
};

export default Input
