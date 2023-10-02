document.addEventListener("DOMContentLoaded", async function () {
    async function getInformationSystems() {
        const response = await fetch("/api/information_systems", {
            method: "GET",
            headers: {"Accept": "application/json"}
        });
        if (response.ok === true) {
            const informationSystems = await response.json();
            const rows = document.querySelector("tbody");

            informationSystems.forEach(infSys => rows.append(setRow(infSys)));
        }
    }

    async function getInformationSystem(id) {
        const response = await fetch(`/api/information_systems/${id}`, {
            method: "GET",
            headers: {"Accept": "application/json"}
        })
        if (response.ok === true) {
            const infSys = await response.json();
            if (document.getElementById("infSysId"))
                document.getElementById("infSysId").value = infSys.id;

            if (document.getElementById("infSysName"))
                document.getElementById("infSysName").value = infSys.name;

            if (document.getElementById("infSysCode"))
                document.getElementById("infSysCode").value = infSys.code;
        } else {
            const error = await response.json();
            console.log(error.message);
        }
    }

    async function editInformationSystem(infSysId, infSysName, infSysCode) {
        const response = await fetch("/api/information_systems", {
            method: "PUT",
            headers: {"Accept": "application/json", "Content-Type": "application/json"},
            body: JSON.stringify({
                id: infSysId,
                name: infSysName,
                code: infSysCode
            })
        });
        if (response.ok === true) {
            const infSys = await response.json();
            document.querySelector(`tr[data-rowid='${infSys.id}']`).replaceWith(setRow(infSys));
        } else {
            const error = await response.json();
            console.log(error.message);
        }
    }

    async function createInformationSystem(infSysName, infSysCode) {
        try{
            const response = await fetch("api/information_systems", {
                method: "POST",
                headers: {"Accept": "application/json", "Content-Type": "application/json"},
                body: JSON.stringify({
                    name: infSysName,
                    code: infSysCode
                })
            });
            if (response.ok === true) {
                const infSys = await response.json();
                document.querySelector("tbody").append(setRow(infSys));
            } else {
                const error = await response.json();
                console.log(error.message);
            }
        }
        catch (error){
            console.log("Failed to create record: ", error)
        }
    }

    async function deleteInformationSystem(id) {
        try{
            const response = await fetch(`/api/information_systems/${id}`, {
                method: "DELETE",
                headers: {"Accept": "application/json"}
            });
            if (response.ok === true) {
                const infSys = await response.json();
                console.log(infSys.id);
                document.querySelector(`tr[data-rowid='${infSys.id}']`).remove();
            } else {
                const error = await response.json();
                console.log(error.message);
            }
        }
        catch (error){
            console.log("An error occurred while deleting a record: ", error)
        }
        
    }

    function reset() {
        if (document.getElementById("infSysId"))
            document.getElementById("infSysId").value = "";
        
        if (document.getElementById("infSysName"))
            document.getElementById("infSysName").value = "";
        
        if (document.getElementById("infSysCode"))
            document.getElementById("infSysCode").value = "";
    }

    function setRow(infSys) {
        const tr = document.createElement("tr");
        tr.setAttribute("data-rowid", infSys.id);

        const codeTd = document.createElement("td");
        codeTd.append(infSys.code);
        tr.append(codeTd);

        const nameTd = document.createElement("td");
        nameTd.append(infSys.name);
        tr.append(nameTd);

        const linksTd = document.createElement("td");

        const editLink = document.createElement("button");
        editLink.append("edit");
        editLink.addEventListener("click", async () => await getInformationSystem(infSys.id));
        linksTd.append(editLink);

        const removeLink = document.createElement("button");
        removeLink.append("remove");
        removeLink.addEventListener("click", async () => await confirmDelete(infSys.id));
        linksTd.append(removeLink);

        tr.appendChild(linksTd);

        return tr;
    }

    async function confirmDelete(id){
        if (confirm("Are you sure you want to delete?")){
            await deleteInformationSystem(id)
            console.log("The record was deleted.")

        } else {
            console.log("Deletion canceled.")
        }
    }

    document.getElementById("resetBtn").addEventListener("click", () => reset());

    document.getElementById("saveBtn").addEventListener("click", async () => {

        // if (document.getElementById("infSysName"))
        const name = document.getElementById("infSysName").value;


        const code = document.getElementById("infSysCode").value;

        if (document.getElementById("infSysId")){
            const id = document.getElementById("infSysId").value;

            if (id === "") {
                await createInformationSystem(name, code);
            } else {
                await editInformationSystem(id, name, code);
            }
            reset();
        }
    });

    await getInformationSystems();
});