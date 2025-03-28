import React from 'react';

const ParticipantItem = ({ participant, index, handlePreferredTeaChange, handleRemoveParticipant }) => {
  return (
    <li className="list-group-item">
      <div className="row align-items-center">
        <div className="col-auto" style={{ width: '150px' }}>
          <span>{participant.name}</span>
        </div>
        <div className="col">
          <form onSubmit={(e) => {
            e.preventDefault();
            handlePreferredTeaChange(index, e.target.elements.preferredTea.value);
          }}>
            <input
              type="text"
              name="preferredTea"
              className="form-control"
              defaultValue={participant.preferredTea}
              placeholder="Preferred Tea"
            />
          </form>
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