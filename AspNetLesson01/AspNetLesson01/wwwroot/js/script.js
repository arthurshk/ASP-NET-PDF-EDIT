
$(window).on('load', function () {

    fetch("/about-me")
        .then(r => r.json())
        .then(aboutMe => {
            $("#about-me h5.card-title").html(`${aboutMe.firstName} ${aboutMe.lastName}`)
            $("#about-me div.info").html(aboutMe.description)

            updateSkills("")
        })

    $("#edit-btn").click(function () {
        $("#edit-form").show();
        $("#first-name").val(aboutMe.firstName);
        $("#last-name").val(aboutMe.lastName);
        $("#description").val(aboutMe.description);
    });
    $("#about-me-form").submit(function (e) {
        e.preventDefault();
        const formData = $(this).serialize();
        $.post("/about-me", formData, function () {
            $("#edit-form").hide();
            location.reload(); 
        });
    });
    $("#save-pdf-btn").click(function () {
        saveAsPDF();
    });
})

let skillsContainer = $('ul#skills')

function saveAsPDF() {
    fetch("/about-me")
        .then(response => response.json())
        .then(aboutMe => {
            const pdf = new jsPDF();
            pdf.setFontSize(16);
            pdf.text(20, 20, "About Me PDF");
            pdf.setFontSize(14);
            pdf.text(20, 40, `Full Name: ${aboutMe.firstName} ${aboutMe.lastName}`);
            pdf.setFontSize(12);
            pdf.text(20, 60, `Description: ${aboutMe.description}`);
            const blob = pdf.output('blob');
            const link = document.createElement('a');
            link.href = URL.createObjectURL(blob);
            link.download = 'about_me.pdf';
            link.click();
        });
}

function updateSkills(query) {
    fetch(`/about-me/skills`, {
        method: "post",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ "query": query })
    })
        .then(r => r.json())
        .then(skills => {
            skillsContainer.empty()
            skills.forEach(s => {
                skillsContainer.append($(`<li><div>${s.title}</div><div class="progress"><div class="progress-bar" role="progressbar" style="width: ${s.experience}%" aria-valuenow="${s.experience}" aria-valuemin="0" aria-valuemax="100"></div></div></li>`))
            })
        })
}

$("input#search").on('input', e => {
    let searchText = $(e.target).val()
    updateSkills(searchText)
})


