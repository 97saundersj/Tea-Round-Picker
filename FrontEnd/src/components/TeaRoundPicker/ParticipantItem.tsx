import React, { useState, ChangeEvent } from 'react';

interface ParticipantItemProps {
  participant: any;
  index: number;
  handlePreferredTeaChange: (id: number, newTea: string) => Promise<void>;
  handleRemoveParticipant: (index: number) => Promise<void>;
}

const ParticipantItem: React.FC<ParticipantItemProps> = ({ 
  participant, 
  index, 
  handlePreferredTeaChange, 
  handleRemoveParticipant 
}) => {
  const [inputValue, setInputValue] = useState<string>(participant.preferredTea || '');

  const handleTeaChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setInputValue(e.target.value);
  };

  const handleBlur = (): void => {
    handlePreferredTeaChange(participant.id, inputValue);
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