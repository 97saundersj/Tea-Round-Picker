import React from 'react';

const ParticipantItem = ({ participant, index, handlePreferredTeaChange, handleRemoveParticipant }) => {
  const handleTeaChange = (e) => {
    const newTea = e.target.value;
    handlePreferredTeaChange(index, newTea);
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
              defaultValue={participant.preferredTea}
              placeholder="Preferred Tea"
              onBlur={handleTeaChange}
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