import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";
import { getAssignedUsers, assignUserToProcedurePlan, removeAssignedUserFromProcedurePlan, removeAllAssignedUserFromProcedurePlan } from "../../../api/api";

const PlanProcedureItem = ({ procedure, users, planId }) => {
    const [selectedUsers, setSelectedUsers] = useState([]);

    const handleAssignUserToProcedure = async (e) => {
        try {
            if (e.length == 0) {
                await removeAllAssignedUserFromProcedurePlan(planId, procedure.procedureId);
            }
            else if (e.length > selectedUsers.length) {
                const addedUser = e.find(user => !selectedUsers.includes(user));
                await assignUserToProcedurePlan(addedUser.value, planId, procedure.procedureId)
            } else {
                const removedUser = selectedUsers.find(user => !e.includes(user));
                await removeAssignedUserFromProcedurePlan(removedUser.value, planId, procedure.procedureId)
            }
            setSelectedUsers(e);
        }
        catch (error) {
            console.log("Unexpected error: "+error.message);
        }
    };

    useEffect(() => {
        (async () => {
            var data = await getAssignedUsers(planId, procedure.procedureId);
            var userOptions = [];
            data.map((d) => userOptions.push({ label: d.user.name, value: d.user.userId }));
            setSelectedUsers(userOptions);
        })();
    }, []);

    return (
        <div className="py-2">
            <div>
                {procedure.procedureTitle}
            </div>

            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                value={selectedUsers}
                onChange={(e) => handleAssignUserToProcedure(e)}
            />
        </div>
    );
};

export default PlanProcedureItem;
