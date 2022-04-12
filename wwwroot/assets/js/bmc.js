export function businessModelCanvas(data){
    const toolbarOptions = {
        container: [
            { header: [1, 2, 3, 4, 5, 6, false] },
            { list: "ordered" },
            { list: "bullet" },
            "bold",
            "italic",
            "underline",
            "strike",
        ],
    };

    let e1, e2, e3, e4, e5, e6, e7, e8, e9;
    e1 = new Quill("#editor-1", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "Who are our Key Partners?",
        theme: "bubble"
    });
    //e1.enable(false);
    //e1.placeholder = null;
    e2 = new Quill("#editor-2", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "What Key Activities do our Value Propositions require?",
        theme: "bubble"
    });

    e3 = new Quill("#editor-3", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "What Key Resources do our Value Propositions require?",
        theme: "bubble"
    });

    e4 = new Quill("#editor-4", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "What value do we deliver to the customer?.",
        theme: "bubble"
    });

    e5 = new Quill("#editor-5", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "What type of relationship does each of our CustomerSegments expect us?",
        theme: "bubble"
    });

    e6 = new Quill("#editor-6", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "Through which Channels do our Customer Segments want to be reached?",
        theme: "bubble"
    });

    e7 = new Quill("#editor-7", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "For whom are we creating value?",
        theme: "bubble"
    });

    e8 = new Quill("#editor-8", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "What are the most important costs inherent in our business model?",
        theme: "bubble"
    });

    e9 = new Quill("#editor-9", {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: "How much does each Revenue Stream contribute to overall revenues?",
        theme: "bubble"
    });


    if (data !== null && data !== "" && data !== undefined) {
        e1.setContents([]);
        e2.setContents([]);
        e3.setContents([]);
        e4.setContents([]);
        e5.setContents([]);
        e6.setContents([]);
        e7.setContents([]);
        e8.setContents([]);
        e9.setContents([]);
        e1.root.innerHTML = data.keyPartners;
        e2.root.innerHTML = data.keyActivities;
        e3.root.innerHTML = data.keyResources;
        e4.root.innerHTML = data.valueProposition;
        e5.root.innerHTML = data.customerRelationship;
        e6.root.innerHTML = data.channels;
        e7.root.innerHTML = data.customerSegments;
        e8.root.innerHTML = data.costStructure;
        e9.root.innerHTML = data.revenueStreams;
        return;
    }

    console.log("2", data);
    if (e1.root.innerHTML !== "<p><br/></p>" && e2.root.innerHTML !== "<p><br/></p>" &&
        e3.root.innerHTML !== "<p><br/></p>" && e4.root.innerHTML !== "<p><br/></p>" &&
        e5.root.innerHTML !== "<p><br/></p>" && e6.root.innerHTML !== "<p><br/></p>" &&
        e7.root.innerHTML !== "<p><br/></p>" && e8.root.innerHTML !== "<p><br/></p>" &&
        e9.root.innerHTML !== "<p><br/></p>") {
        var KeyPartners = e1.root.innerHTML;
        var KeyActivities = e2.root.innerHTML;
        var KeyResources = e3.root.innerHTML;
        var ValueProposition = e4.root.innerHTML;
        var CustomerRelationship = e5.root.innerHTML;
        var Channels = e6.root.innerHTML;
        var CustomerSegments = e7.root.innerHTML;
        var CostStructure = e8.root.innerHTML;
        var RevenueStreams = e9.root.innerHTML;
        return {
            KeyPartners,
            KeyActivities,
            KeyResources,
            ValueProposition,
            CustomerRelationship,
            Channels,
            CustomerSegments,
            CostStructure,
            RevenueStreams
        }
    } else {
        return;
    }
}