import React, { useEffect, useState } from 'react';
import axios from 'axios';

const PreviousSelectionsTable = ({ teamId, refresh }) => {
  const [selections, setSelections] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchSelections = async () => {
      if (teamId) {
        try {
          const response = await axios.get(`${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}/previous-participant-selections`);
          setSelections(response.data);
        } catch (err) {
          setError('Error fetching previous selections.');
        }
      }
    };

    fetchSelections();
  }, [teamId, refresh]);

  if (!teamId || selections.length === 0 || error) return null;

  return (
    <div className="card mt-4">
      <div className="card-body">
        <h2 className="card-title mb-4">Previous Tea Rounds</h2>
          <table className="table table-striped">
            <thead className="thead-dark">
              <tr>
                <th>Date</th>
                <th>Participants</th>
                <th>Chosen Participant</th>
              </tr>
            </thead>
            <tbody>
              {selections.map((selection) => (
                <tr key={selection.id}>
                  <td>{new Date(selection.date).toLocaleString()}</td>
                  <td>{selection.participants.map((participant) => (participant.name)).join(', ')}</td>
                  <td>{selection.chosenParticipant}</td>
                </tr>
              ))}
            </tbody>
          </table>
      </div>
    </div>
  );
};

export default PreviousSelectionsTable; 