import React, { useState } from 'react';

const ParticipantItem = ({ participant, index, handlePreferredTeaChange, handleRemoveParticipant }) => {
  const [inputValue, setInputValue] = useState(participant.preferredTea);

  const handleTeaChange = (e) => {
    setInputValue(e.target.value);
  };

  const handleBlur = () => {
    handlePreferredTeaChange(index, inputValue);
  };

  return (
    <li className="list-group-item">
      <div className="row align-items-center">
        <div className="col-3">
          <span>{participant.name}</span>
        </div>
        <div className="col">
            <input
              type="text"
              name="preferredTea"
              className="form-control"
              value={inputValue}
              placeholder="Preferred Tea"
              onChange={handleTeaChange}
              onBlur={handleBlur}
            />
        </div>
        <div className="col-auto">
          <button
            className="btn btn-sm btn-danger"
            onClick={() => handleRemoveParticipant(index)}
          >
            Remove
          </button>
        </div>
      </div>
    </li>
  );
};

export default ParticipantItem; 