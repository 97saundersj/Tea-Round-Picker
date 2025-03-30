import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { TeaRound, TeaRoundsResponse } from '../../types/Types';

interface TeaRoundsTableProps {
  teamId: number | null;
  refresh: boolean;
}

const TeaRoundsTable: React.FC<TeaRoundsTableProps> = ({ teamId, refresh }) => {
  const [selections, setSelections] = useState<TeaRound[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchSelections = async (): Promise<void> => {
      if (teamId) {
        try {
          const response = await axios.get<TeaRoundsResponse>(
            `${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}/previous-participant-selections`
          );
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
    <div className="card m-2">
      <div className="card-body">
        <h5 className="card-title mb-4">Previous Tea Rounds</h5>
        <div className="accordion" id="teaRoundAccordion">
          {selections.map((selection, index) => (
            <div className="accordion-item" key={selection.id}>
              <h2 className="accordion-header" id={`heading${index}`}>
                <button
                  className="accordion-button collapsed"
                  type="button"
                  data-bs-toggle="collapse"
                  data-bs-target={`#collapse${index}`}
                  aria-expanded="false"
                  aria-controls={`collapse${index}`}
                >
                  <b>
                    {new Intl.DateTimeFormat('en-UK', { dateStyle: 'medium', timeStyle: 'short' }).format(new Date(selection.date))} - {selection.chosenParticipant.name} made tea
                  </b>
                </button>
              </h2>
              <div
                id={`collapse${index}`}
                className="accordion-collapse collapse"
                aria-labelledby={`heading${index}`}
              >
                <div className="accordion-body">
                  <table className="table table-hover">
                    <thead className="thead-dark">
                      <tr>
                        <th>Participant</th>
                        <th>Tea Order</th>
                      </tr>
                    </thead>
                    <tbody>
                      {selection.teaOrders?.map((teaOrder) => (
                        <tr key={teaOrder.id}>
                          <td>{teaOrder.participant.name}</td>
                          <td>{teaOrder.requestedTeaOrder || <small className="text-muted fst-italic">None Specified</small>}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default TeaRoundsTable; 